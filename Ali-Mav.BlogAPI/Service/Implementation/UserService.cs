﻿using Ali_Mav.BlogAPI.Data.Interfaces;
using Ali_Mav.BlogAPI.Models;
using Ali_Mav.BlogAPI.Models.DTO;
using Ali_Mav.BlogAPI.Models.Response;
using Ali_Mav.BlogAPI.Service.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Reflection;

namespace Ali_Mav.BlogAPI.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<List<UserViewModel>>> AddUsers(List<UserViewModel> users)
        {
            var serviceResponse = new BaseResponse<List<UserViewModel>>();
            try
            {
                var userDb = _userRepository.GetAll();
                if (!userDb.Any())
                {
                    
                    var userList = _mapper.Map<List<UserViewModel>,List<User>>(users);
                    await _userRepository.AddUsers(userList);

                    serviceResponse.success = true;
                    serviceResponse.Data = users;

                }
                else
                {
                    serviceResponse.Description = "There is already a user in the database";
                    serviceResponse.success = false;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Description = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<BaseResponse<List<User>>> GetAll()
        {
            var serviceResponse = new BaseResponse<List<User>>(); 

            try
            {
                var users = _userRepository.GetAll();
                if (!users.Any())
                {
                    serviceResponse.success = false;
                    serviceResponse.Description = "Users not found";
                }
                else
                {
                    serviceResponse.success = true;
                    serviceResponse.Data = users;
                }

            }
            catch (Exception ex)
            {
                serviceResponse.Description= ex.Message;
                serviceResponse.success= false;
            }

            return serviceResponse;
        }

        public async Task<BaseResponse<User>> GetById(int userId)
        {
            var serviceResponse = new BaseResponse<User>();

            try
            {
                var user = await _userRepository.GetById(userId);
                if (user == null)
                {
                    serviceResponse.success = true;
                    serviceResponse.Description = "User not found";
                }
                else
                {
                    serviceResponse.success = true;
                    serviceResponse.Data = user;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Description = ex.Message;
                serviceResponse.success = false;
            }
            return serviceResponse;
        }

        public async Task<BaseResponse<List<User>>> Search(string word)
        {
            var serviceResponse = new BaseResponse<List<User>>();
            try
            {
                if (string.IsNullOrWhiteSpace(word))
                {
                    serviceResponse.success = false;
                    serviceResponse.Description = "Enter a word or letter";
                }
                else
                {
                    var user = await _userRepository.SearchAsync(word);
                    serviceResponse.success = true;
                    serviceResponse.Data = user;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Description = ex.Message;
                serviceResponse.success = false;
            }
            return serviceResponse;
        }
    }
}
