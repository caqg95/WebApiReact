using System;
using System.Linq;
using System.Web.Http;

using WebApiReact.Models;

namespace ReactCRUDApi.Controllers
{
    [RoutePrefix("Api/User")]
    public class UserController : ApiController
    {
        ReactEntities objEntity = new ReactEntities();

        [HttpGet]
        [Route("GetUserDetails")]
        public IQueryable<UserDetail> GetEmaployee()
        {
            try
            {
                return objEntity.UserDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetUserDetailsById/{userId}")]
        public IHttpActionResult GetUserById(string userId)
        {
            UserDetail objUser = new UserDetail();
            int ID = Convert.ToInt32(userId);
            try
            {
                objUser = objEntity.UserDetails.Find(ID);
                if (objUser == null)
                {
                    return NotFound();
                }

            }
            catch (Exception)
            {
                throw;
            }

            return Ok(objUser);
        }

        [HttpPost]
        [Route("InsertUserDetails")]
        public IHttpActionResult PostUser(UserDetail data)
        {
            string message = "";
            if (data != null)
            {

                try
                {
                    objEntity.UserDetails.Add(data);
                    int result = objEntity.SaveChanges();
                    if (result > 0)
                    {
                        message = "User has been sussfully added";
                    }
                    else
                    {
                        message = "faild";
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return Ok(message);
        }

        [HttpPut]
        [Route("UpdateEmployeeDetails")]
        public IHttpActionResult PutUserMaster(UserDetail user)
        {
            string message = "";
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                UserDetail objUser = new UserDetail();
                objUser = objEntity.UserDetails.Find(user.UserId);
                if (objUser != null)
                {
                    objUser.FirstName = user.FirstName;
                    objUser.LastName = user.LastName;
                    objUser.EmailId = user.EmailId;
                    objUser.MobileNo = user.MobileNo;
                    objUser.Address = user.Address;
                    objUser.PinCode = user.PinCode;

                }

                int result = objEntity.SaveChanges();
                if (result > 0)
                {
                    message = "User has been sussfully updated";
                }
                else
                {
                    message = "faild";
                }

            }
            catch (Exception)
            {
                throw;
            }

            return Ok(message);
        }

        [HttpDelete]
        [Route("DeleteUserDetails/{id}")]
        public IHttpActionResult DeleteUserDelete(int id)
        {
            string message = "";
            UserDetail user = objEntity.UserDetails.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            objEntity.UserDetails.Remove(user);
            int result = objEntity.SaveChanges();
            if (result > 0)
            {
                message = "User has been sussfully deleted";
            }
            else
            {
                message = "faild";
            }

            return Ok(message);
        }
    }
}