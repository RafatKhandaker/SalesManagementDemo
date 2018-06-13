using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using SalesManagement.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace SalesManagement.UI.Models
{
    public class UserDetailsModel
    {
        /// <summary>
        /// // User Table 
        /// </summary>
        /// 

        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PassWord { get; set; }
        [ForeignKey("Roles")]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        /// <summary>
        /// // User Detail table 
        /// </summary>


        [Key]
        public int inUserDetailId { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        //public virtual User User { get; set; }
    }

    //public class UserContext : DbContext
    //{
    //    public DbSet<User> User { get; set; }
    //    public DbSet<User_Details> User_Details { get; set; }
    //}

}