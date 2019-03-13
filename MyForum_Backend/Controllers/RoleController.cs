using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyForum_Backend.Models;
using MyForum_Backend.Models.Binding_Models;

namespace MyForum_Backend.Controllers
{
    [RoutePrefix("api/Roles")]
    [Authorize(Roles = "Super admin")]
    public class RoleController : ApiController
    {
        private static ApplicationDbContext context = new ApplicationDbContext();

        private RoleManager<IdentityRole> _roleManager = 
            new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

        // GET api/Roles
        [Route("")]
        [HttpGet]
        public IQueryable<IdentityRole> GetRoles()
        {
            return _roleManager.Roles;
        }
       
        // POST api/Roles
        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> PostRole([FromBody]RoleBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(model.Name));

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // DELETE api/Roles/{id}
        [Route("{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }

                return Ok();
            }

            return NotFound();

        }

        // PUT api/Roles/{id}
        [Route("{id}")]
        [HttpPut]
        public async Task<IHttpActionResult> PutRole(string id, [FromBody]RoleBindingModel model)
        {
            var role = await _roleManager.FindByIdAsync(id);
            role.Name = model.Name;

            if (role != null)
            {
                IdentityResult result = await _roleManager.UpdateAsync(role);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }

                return Ok();
            }

            return NotFound();

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _roleManager.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Helpers
        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        } 
        #endregion
    }
}
