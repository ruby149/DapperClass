﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IntroSQL
{
   public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAllDepartments(); 

    }
}
