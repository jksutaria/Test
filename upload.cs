        [HttpPost]
        public ActionResult EditPerfil(HttpPostedFileBase file, string nombre, int UserID = 0, string contacto="", 
            string email = "", string fnac="")
        {
            string oripath = "";
            string path = "";
            string pathcomment = "";
            string pathgrupo = "";
            string strExt = "";
            string name = "";

            if (file != null)
            {
                name = Path.GetFileName(file.FileName);
                strExt = Path.GetExtension(name);

                oripath = Path.Combine(Server.MapPath("~/Content/images/profiles/"), name); // Ruta Original
                file.SaveAs(oripath);
                var img = Image.FromFile(oripath);

                path = Path.Combine(Server.MapPath("~/Content/images/profiles/"), UserID + strExt); // Ruta para imagen de perfil
                var img2 = new Bitmap(HomeController.ScaleImage(img, 50, 50, true));
                img2.Save(path);

                pathcomment = Path.Combine(Server.MapPath("~/Content/images/profiles/"), "c_" + UserID + strExt); // Ruta para imagen de comentarios
                var img3 = new Bitmap(HomeController.ScaleImage(img, 30, 30, true));
                img3.Save(pathcomment);

                pathgrupo = Path.Combine(Server.MapPath("~/Content/images/profiles/"), "g_" + UserID + strExt); // Ruta para imagen de grupos
                var img4 = new Bitmap(HomeController.ScaleImage(img, 100, 100, true));
                img4.Save(pathgrupo);

                //System.IO.File.Delete(oripath);
            }

            User u = db.User.Single(y => y.UserID == UserID);
            //u.nombre = nombre;
            //u.nroContacto = contacto;
            //u.fecha_nacimiento = Convert.ToDateTime(fnac);
            //u.email = email;

            if (file != null)
            {
                u.imagen = UserID + strExt;
            }
            db.SaveChanges();

            Session["User"] = u;

            return RedirectToAction("Principal", "Home", new { UserID = UserID });
        }