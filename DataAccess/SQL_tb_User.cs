using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TraCuuDuLieu.Business.EntitiesClass;

namespace TraCuuDuLieu.DataAccess
{
    class SQL_tb_User
    {
        ConnectDB cn = new ConnectDB();

        public bool Kiemtrauser(EC_tb_User user)
        {
            string sql = "select count(*) from account_tracuu with(nolock) where Username ='" + user.USERNAME + "' and userpassword = '" + user.PASSWORD + "'";
            return cn.KiemtraUsername(sql);
        }
    }
}
