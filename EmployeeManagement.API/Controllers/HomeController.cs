﻿using EmployeeManagement.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Xml;
using WebAPI.BLL.Core.IConfiguration;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HomeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] UserCred userCred)
        {
            if (userCred == null) throw new ArgumentNullException(nameof(userCred));

            try
            {
                var result = await _unitOfWork.EmployeeRepository.AuthenticateUser(userCred);

                if (result.BearerToken != null)
                {
                    return Ok(result);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException(String.Format("Error in function: {0} - \n Error Message:{1} - \n Stack Trace:{2}",
                                                               System.Reflection.MethodBase.GetCurrentMethod().Name,
                                                               exception.Message, exception?.ToString()));
            }


        }




        [HttpGet("UploadYourData")]
        [Authorize]
        public async Task<IActionResult> UploadYourData(string userId)
        {
            var res = await _unitOfWork.EmployeeRepository.UploadDataToAzure(userId);

            if (res)
            {
                return Ok("Operation Success");
            }
            else
            {
                return Unauthorized();
            }


        }

        [Produces("application/xml")]
        [HttpGet("DownloadYourData")]
        [Authorize]
        public IActionResult DownloadYourData(string userId)
        {
            try
            {
                var res = _unitOfWork.EmployeeRepository.DownloadDataFromAzure(userId);

                if (res != null)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(res);

                    return Ok(doc);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex) when (ex.Message.Contains("File Exists"))
            {
                XmlDocument doc = new XmlDocument();
                var array = ex.Message.Split("@");
                doc.Load(array[1]);
                return Ok(doc);
            }

        }


        [HttpGet("GetSpecificUserData")]
        [Authorize]
        public async Task<IActionResult> GetSpecificUserData([FromQuery] string userId)
        {
            try
            {
                var userData = await _unitOfWork.EmployeeRepository.GetUserData(userId);
                return Ok(userData);
            }
            catch (Exception ex) when (ex.Message.Contains("User does not exists"))
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpGet("GetUserData")]
        [Authorize]
        public IActionResult GetUserData([FromQuery] CursorParams @params)
        {
            try
            {
                int? nextCursor = 0;
                var userData = _unitOfWork.EmployeeRepository.GetUserData(@params, out nextCursor);

                Response.Headers.Add("X-Pagination", $"Next-Cursor = {nextCursor}");
                return Ok(userData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }

}