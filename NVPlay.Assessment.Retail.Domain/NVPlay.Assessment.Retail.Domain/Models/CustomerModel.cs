using NVPlay.Assessment.Retail.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVPlay.Assessment.Retail.Domain.Models
{
    public class CustomerModel: ICustomer
    {
        private bool _isClubMember;
        public CustomerModel(bool isClubMember)
        {
            _isClubMember = isClubMember;   
        }

        public bool IsClubMember() 
        { 
            return _isClubMember; 
        }
    }
}
