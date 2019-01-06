using Dapper;
using FNMusicUserApiLibrary.Models;
using FNMusicUserApiLibrary.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using FNMusicUserApiLibrary.ServiceResponses;
using FNMusicUserApiLibrary.Services;

namespace FNMusicUserApiLibrary.Dao
{
    public class UserDao : DbConfig
    {
        private static IConfiguration Configuration;
        public UserDao(IConfiguration configuration) : base(configuration)
        {
            Configuration = configuration;
        }

        public static async Task<bool> VerifyUsername(string Username)
        {
            try
            {
                using (var conn = Connection)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Username", Username);
                    System.Data.IDataReader reader = await conn.ExecuteReaderAsync("VerifyUsernameProcedure", parameters, commandType: System.Data.CommandType.StoredProcedure);
                    while (reader.Read())
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        public static async Task<SignupResponse> SignupAsync(SignupRequest signupRequest)
        {
            try
            {
                if (!await VerifyUsername(signupRequest.Username))
                {
                    using (var conn = Connection)
                    {
                        conn.Open();
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("UserId", signupRequest.UserId);
                        parameters.Add("Username", signupRequest.Username);
                        parameters.Add("FirstName", signupRequest.FirstName);
                        parameters.Add("LastName", signupRequest.LastName);
                        parameters.Add("Email", signupRequest.EmailAddress);
                        parameters.Add("PasswordHash", GetHash.HashCode(signupRequest.Password));
                        parameters.Add("Gender", signupRequest.Gender);
                        parameters.Add("BirthDate", signupRequest.BirthDate);
                        parameters.Add("Nationality", signupRequest.Nationality);
                        parameters.Add("PhoneNumber", signupRequest.PhoneNumber);
                        parameters.Add("Location", signupRequest.Location);
                        parameters.Add("PrimaryGenre", signupRequest.PrimaryGenre);
                        parameters.Add("Biography", signupRequest.Biography);
                        parameters.Add("Website", signupRequest.Website);
                        parameters.Add("ProfileImagePath", signupRequest.ProfileImagePath);
                        parameters.Add("CoverImagePath", signupRequest.CoverImagePath);
                        parameters.Add("DateCreated", signupRequest.DateCreated);

                        await conn.ExecuteAsync("SignupProcedure", parameters, commandType: System.Data.CommandType.StoredProcedure);
                        return new SignupResponse(true, new List<ServiceResponse>
                        {
                            new ServiceResponse("200", "Successful", null)
                        });
                    }
                }
                else
                {
                    return new SignupResponse(false, new List<ServiceResponse>
                    {
                        new ServiceResponse("200","Username already exists", null)
                    });
                }
                
            }
            catch (Exception ex)
            {
                return new SignupResponse(false, new List<ServiceResponse>
                {
                    new ServiceResponse("400", "Error Signing Up", new List<Error>()
                    {
                        new Error(ex.GetHashCode().ToString(), ex.Message)
                    })
                });
            }

        }

        public static async Task<SigninResponse> SigninAsync(LoginRequest loginRequest)
        {
            int AccessCountFailed = 0;
            string DbUsername = "", DbPasswordhash = "", Message = "LockedOut";
            try
            {
                using (var conn = Connection)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("Username", loginRequest.Username);
                    System.Data.IDataReader reader = await conn.ExecuteReaderAsync("SigninProcedure", parameters, commandType: System.Data.CommandType.StoredProcedure);
                    while (reader.Read())
                    {
                        DbUsername = reader["Username"].ToString();
                        DbPasswordhash = reader["PasswordHash"].ToString();
                        AccessCountFailed = reader.GetInt32(2);
                        break;
                    }
                    reader.Close();

                    while (AccessCountFailed < 5)
                    {
                        if ((loginRequest.Username == DbUsername) && (GetHash.HashCode(loginRequest.Password) == DbPasswordhash))
                        {
                            Message = "true";
                        }                           
                        else
                        {                
                            //await conn.ExecuteAsync("AccessFailedCountIncrement",parameters, commandType: System.Data.CommandType.StoredProcedure);
                            Message = "false";
                        }              
                        break;
                    }

                    return new SigninResponse(true, new List<ServiceResponse>
                    {
                        new ServiceResponse("200", Message, null)
                    });
                }
            }
            catch (Exception ex)
            {
                return new SigninResponse(false, new List<ServiceResponse>
                {
                    new ServiceResponse("400", "Error Logging In", new List<Error>()
                    {
                        new Error(ex.GetHashCode().ToString(), ex.Message)
                    })
                });
            }
        }

        public static async Task<EditPhoneNoResponse> EditPhoneNumberAsync(EditPhoneNoRequest editPhoneNoRequest)
        {
            try
            {
                var loginRequest = new LoginRequest(editPhoneNoRequest.Username, editPhoneNoRequest.Password);    
                SigninResponse signinResponse = await SigninAsync(loginRequest);
                if (signinResponse.isLoggedIn == true)
                {
                    using (var conn = Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("Username", editPhoneNoRequest.Username);
                        parameters.Add("PhoneNumber", editPhoneNoRequest.PhoneNumber);
                        await conn.ExecuteAsync("EditPhoneNoProcedure", parameters, commandType: System.Data.CommandType.StoredProcedure);
                        return new EditPhoneNoResponse(editPhoneNoRequest.PhoneNumber,true, new List<ServiceResponse>{
                            new ServiceResponse( "200", "Successful", null)
                        });
                    }
                }
                else
                {
                    return new EditPhoneNoResponse(editPhoneNoRequest.PhoneNumber,false, new List<ServiceResponse> {
                        new ServiceResponse("200", "Authentication Error", null)
                    });
                }

                
            }
            catch (Exception ex)
            {
                return new EditPhoneNoResponse(editPhoneNoRequest.PhoneNumber, false, new List<ServiceResponse>{
                    new ServiceResponse("400", "Error Updating your PhoneNumber", new List<Error>() {
                      new Error(ex.GetHashCode().ToString(),ex.Message)
                    })
                });
            }
            
        }

        public static async Task<LockAccountResponse> LockAccountAsync(LockAccountRequest lockAccountRequest)
        {
            try
            {
                var loginRequest = new LoginRequest(lockAccountRequest.Username, lockAccountRequest.Password);
                SigninResponse signinResponse = await SigninAsync(loginRequest);
                if (signinResponse.isLoggedIn == true)
                {
                    using (var conn = Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("Username",lockAccountRequest.Username);
                        await conn.ExecuteAsync("LockAccountProcedure", parameters, commandType: System.Data.CommandType.StoredProcedure);
                        return new LockAccountResponse(lockAccountRequest.Username, true, new List<ServiceResponse>{
                            new ServiceResponse("200","Successful",null)
                        });
                    }
                }
                else
                {
                    return new LockAccountResponse(lockAccountRequest.Username, false, new List<ServiceResponse>
                    {
                        new ServiceResponse("200","Authentication Error",null)
                    });
                }
                
            }
            catch (Exception ex)
            {
                return new LockAccountResponse(lockAccountRequest.Username, false, new List<ServiceResponse> {
                    new ServiceResponse("400", "Something went wrong", new List<Error> {
                        new Error(ex.GetHashCode().ToString(), ex.Message)
                    })
                });
            }
        }

        public static async Task<ConfirmEmailResponse> ConfirmEmailAsync(string UserId)
        {
            string Username = "";
            string EmailAddress = "";
            try
            {
                using(var conn = Connection)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("Userid", UserId);
                    System.Data.IDataReader reader =  await conn.ExecuteReaderAsync("ConfirmEmailProcedure", parameters, commandType: System.Data.CommandType.StoredProcedure);
                    while (reader.Read())
                    {
                        Username = reader["Username"].ToString();
                        EmailAddress = reader["Email"].ToString();
                    }
                    return new ConfirmEmailResponse(Username, EmailAddress, true, new List<ServiceResponse> {
                        new ServiceResponse("200","Email was Confirmed Successfully", null)
                    });
                }
            }
            catch (Exception ex)
            {
                return new ConfirmEmailResponse(Username, EmailAddress, false, new List<ServiceResponse> {
                    new ServiceResponse("400","Something went wrong", new List<Error>{
                        new Error(ex.GetHashCode().ToString(),ex.Message)
                    })
                });
            }
        }

        public static async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest forgotPasswordRequest)
        {
            string resetLink = "";
            string IdHash = "";
            string phoneNumber = "";
            string email = "";
            try
            {
                using (var conn = Connection)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("Username", forgotPasswordRequest.Username);
                    System.Data.IDataReader reader = await conn.ExecuteReaderAsync("ForgotPasswordProcedure", parameters, commandType: System.Data.CommandType.StoredProcedure);
                    while (reader.Read())
                    {
                        phoneNumber = reader["PhoneNumber"].ToString();
                        email = reader["Email"].ToString();
                        IdHash = UserIdGenerator.RandomGen(30);
                        resetLink = "https://localhost:5001/api/User/ResetPassword/" +  IdHash + "";
                        parameters = new DynamicParameters();
                        parameters.Add("UserId", reader["UserId"].ToString());
                        parameters.Add("URLHashCode", IdHash);
                        reader.Close();
                        await conn.ExecuteAsync("ForgotPasswordUrlProcedure", parameters, commandType: System.Data.CommandType.StoredProcedure);
                        break;
                    }
                    return new ForgotPasswordResponse(forgotPasswordRequest.Username,email,phoneNumber,true, resetLink, new List<ServiceResponse>
                    {
                        new ServiceResponse("200","Link was Succesfully sent to your device",null)
                    });
                }
            } 
            catch (Exception ex)
            {
                return new ForgotPasswordResponse(forgotPasswordRequest.Username, email, phoneNumber, false, null, new List<ServiceResponse> {
                    new ServiceResponse("200","Something went wrong", new List<Error>
                    {
                        new Error(ex.GetHashCode().ToString(),ex.Message)
                    })
                });
            }
        }

        public static async Task<ResetPasswordResponse> ResetPasswordAsync(string refLink, ResetPasswordRequest resetPasswordRequest)
        {
            try
            {
                using (var conn = Connection)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("refLink",refLink);
                    parameters.Add("NewPasswordHash", GetHash.HashCode(resetPasswordRequest.NewPassword));
                    await conn.ExecuteAsync("ResetPasswordProcedure",parameters,commandType: System.Data.CommandType.StoredProcedure);
                    return new ResetPasswordResponse(true, new List<ServiceResponse>
                    {
                        new ServiceResponse("200","Your Password was reset successfully",null)
                    });
                }
            }
            catch (Exception ex)
            {
                return new ResetPasswordResponse(false, new List<ServiceResponse>
                {
                    new ServiceResponse("400","Something went wrong", new List<Error>
                    {
                        new Error(ex.GetHashCode().ToString(),ex.Message)
                    })
                });
            }
        }
    }
}
