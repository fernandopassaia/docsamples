using System;
using System.Linq;
using System.Web.Http;
using CRUDAPI.Models;

namespace CRUDAPI.Controllers
{
    //[RoutePrefix("Api/Employee")]
    public class EmployeeController : ApiController
    {
        WebApiDbEntities objEntity = new WebApiDbEntities();
        
        [HttpGet]
        [Route("api/employees")]
        public IQueryable<EmployeeDetail> Get()
        {
            try
            {
                return objEntity.EmployeeDetails;
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("api/employees/{id}")]
        public IHttpActionResult GetById(int id)
        {
            EmployeeDetail objEmp = new EmployeeDetail();
            int ID = Convert.ToInt32(id);
            try
            {
                 objEmp = objEntity.EmployeeDetails.Find(ID);
                if (objEmp == null)
                {
                    return NotFound();
                }

            }
            catch (Exception)
            {
                throw;
            }
           
            return Ok(objEmp);
        }

        [HttpPost]
        [Route("api/employees")]
        public IHttpActionResult Post(EmployeeDetail data)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                objEntity.EmployeeDetails.Add(data);
                objEntity.SaveChanges();
            }
            catch(Exception)
            {
                throw;
            }



            return Ok(data);
        }
        
        [HttpPut]
        [Route("api/employees")]
        public IHttpActionResult Put(EmployeeDetail employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                EmployeeDetail objEmp = new EmployeeDetail();
                objEmp = objEntity.EmployeeDetails.Find(employee.EmpId);
                if (objEmp != null)
                {
                    objEmp.EmpName = employee.EmpName;
                    objEmp.Address = employee.Address;
                    objEmp.EmailId = employee.EmailId;
                    objEmp.DateOfBirth = employee.DateOfBirth;
                    objEmp.Gender = employee.Gender;
                    objEmp.PinCode = employee.PinCode;

                }
                int i = this.objEntity.SaveChanges();

            }
            catch(Exception)
            {
                throw;
            }
            return Ok(employee);
        }
        [HttpDelete]
        [Route("api/employees/{id}")]
        public IHttpActionResult Delete(int id)
        {
            //int empId = Convert.ToInt32(id);
            EmployeeDetail emaployee = objEntity.EmployeeDetails.Find(id);
            if (emaployee == null)
            {
                return NotFound();
            }

            objEntity.EmployeeDetails.Remove(emaployee);
            objEntity.SaveChanges();

            return Ok(emaployee);
        }
    }
}
