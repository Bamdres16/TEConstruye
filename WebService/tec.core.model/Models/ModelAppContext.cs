using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace tec.core.model.Models
{
    class ModelAppContext : DbContext
    {
        public ModelAppContext (DbContextOptions <ModelAppContext> options) : base(options)
        {

        }
    }
}
