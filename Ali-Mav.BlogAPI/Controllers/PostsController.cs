﻿using Ali_Mav.BlogAPI.Models;
using Ali_Mav.BlogAPI.Models.DTO;
using Ali_Mav.BlogAPI.Models.Response;
using Ali_Mav.BlogAPI.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ali_Mav.BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Post>> Create(PostCreateDto postDto)
        {

            var response = await _postService.CreatePost(postDto);
            if (response.success)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.Description);
        }
        
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<PostGetDto>>> GetAll() 
        {
            var response = await _postService.GetAll();
            if (response.success)
            {
                return Ok(response.Data);
            }
            
            return BadRequest(response.Description);
        }

        [HttpGet("GetUserPosts")]
        public async Task<ActionResult<List<Post>>> GetUserPosts(int userId)
        {
            var response = await _postService.GetUserPosts(userId);

            if (response.success)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.Description);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<PostGetDto>> Update(PostUpdateDto postDto)
        {
            var response = await _postService.UpdatePost(postDto);
            if (response.success)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.Description);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            var response =await _postService.DeletePost(id);
            if (response.success)
            {
                return Ok(response.success);
            }

            return BadRequest(response.Description);
        }

        [HttpGet("Page")]
        public async Task<ActionResult<List<Post>>> GetPaging(int pageSize, int pagenumber)
        { 
            var response = await _postService.GetPaging(pageSize, pagenumber);
            if (response.success)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.Description);
        }
    }
}
