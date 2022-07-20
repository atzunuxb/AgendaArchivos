using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EjericioFinal_AgendaConArchivos
{
    class Contacto
    {
        public  string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Localidad { get; set; }
        public string Codigo { get; set; }



        static string nombre;
        static string apellido;
        static string telefono;
        static string localidad;
        static string opcion;
        static string codigo;
        static List<string> contactoList = new List<string>();
        public static void AgregarContacto()
        {
            Console.WriteLine("Por favor ingresar los datos");
            Console.WriteLine("Nombre");
            nombre = Console.ReadLine();
            Console.WriteLine("Apellido");
            apellido = Console.ReadLine();
            Console.WriteLine("Telefono");
            telefono = Console.ReadLine();
            Console.WriteLine("Localidad");
            localidad = Console.ReadLine();
            Console.WriteLine("Codigo");
            codigo = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("\nDatos ingresados");
            Console.WriteLine("Nombre: " +nombre);
            Console.WriteLine("Apellido: "+apellido);
            Console.WriteLine("Telefono: "+telefono);
            Console.WriteLine("Localidad: "+localidad);
            Console.WriteLine("Codigo: "+codigo);

            Console.WriteLine("\nGuardar datos");
            Console.WriteLine("Presionar S para Guardar");
            Console.WriteLine("Presionar N para cancelar");
            opcion = Console.ReadLine();
            opcion = opcion.ToUpper();

            if (opcion == "S")
            {
                FileStream crearAbrir = new FileStream("contactos.txt", FileMode.OpenOrCreate);
                crearAbrir.Close();

                FileStream agregar = new FileStream("contactos.txt", FileMode.Append);
                string contacto = $"{nombre}|{apellido}|{telefono}|{localidad}|{codigo};";
                agregar.Write(ASCIIEncoding.ASCII.GetBytes(contacto),0,contacto.Length);
                Console.Clear();
                Console.WriteLine("Datos Guardados Correctamente");
                agregar.Close();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("No se guardaron los datos");
            }
     
        }


        public static bool ContactoPorNombre()
        {
            int numeroContacto = 1;
            Console.WriteLine("Ingresar el nombre del contacto");
            nombre = Console.ReadLine();

            byte[] infoContacto = new byte[500000];
            FileStream abir = new FileStream("contactos.txt", FileMode.Open);
            abir.Read(infoContacto, 0, (int)abir.Length);
            abir.Close();

            var datos = ASCIIEncoding.ASCII.GetString(infoContacto);
            var contactos = datos.Split(';').ToList();


            contactos.RemoveAt(contactos.Count - 1);
            foreach (var item in contactos.Where(con => con.Contains(nombre)))
            {
                var contacto = ExtraerContacto(item);

                    contactoList.Add(
                        $"\nDatos del contacto: {numeroContacto}\n" +
                        $"Nombre: {contacto.Nombre}\n" +
                        $"Apellido: {contacto.Apellido}\n" +
                        $"Telefono: {contacto.Telefono}\n" +
                        $"Localidad: {contacto.Localidad}\n" +
                        $"Codigo: {contacto.Codigo}\n");
                    numeroContacto++;
            }

            if (contactoList.Any())
            {
               foreach (var item in contactoList)
                {
                    Console.WriteLine(item);
                }
                return true;
            }

            else
            {
                Console.WriteLine("No se encontro ninguna contacto con el dato ingresado");
                return false;
            }
        }

        public static bool ContactoPorCodigo(string pCodigo)
        {
            int numeroContacto = 1;
            string cod = pCodigo;
            byte[] infoContacto = new byte[500000];
            FileStream abir = new FileStream("contactos.txt", FileMode.Open);
            abir.Read(infoContacto, 0, (int)abir.Length);
            abir.Close();

            var datos = ASCIIEncoding.ASCII.GetString(infoContacto);
            var contactos = datos.Split(';').ToList();




            contactos.RemoveAt(contactos.Count - 1);
            foreach (var item in contactos)
            {
                var contacto = ExtraerContacto(item);

                if(cod == contacto.Codigo)
                {
                    contactoList.Add(
                    // $"\nDatos del contacto: {numeroContacto}\n" +
                    $"Nombre: {contacto.Nombre}\n" +
                    $"Apellido: {contacto.Apellido}\n" +
                    $"Telefono: {contacto.Telefono}\n" +
                    $"Localidad: {contacto.Localidad}\n" +
                    $"Codigo: {contacto.Codigo}\n");
                    numeroContacto++;
                }
                
            }

            if (contactoList.Any())
            {
                foreach (var item in contactoList)
                {
                    Console.WriteLine(item);
                }
                return true;
            }

            else
            {
                Console.WriteLine("No se encontro ninguna contacto con el dato ingresado");
                return false;
            }
        }

        public static void ContactoPorCodigoSimple(string pCodigo)
        {
            string cod = pCodigo;
            byte[] infoContacto = new byte[500000];
            FileStream abir = new FileStream("contactos.txt", FileMode.Open);
            abir.Read(infoContacto, 0, (int)abir.Length);
            abir.Close();
            var datos = ASCIIEncoding.ASCII.GetString(infoContacto);
            var contactos = datos.Split(';').ToList();

            contactos.RemoveAt(contactos.Count - 1);
            foreach (var item in contactos)
            {
                var contacto = ExtraerContacto(item);
                if (cod == contacto.Codigo)
                {
                    Console.WriteLine($"Nombre: {contacto.Nombre}");
                    Console.WriteLine($"Apellido: {contacto.Apellido}");
                    //Console.WriteLine($"Telefono: {contacto.Telefono}");
                    Console.WriteLine($"Localidad: {contacto.Localidad}\n");
               
                }

            }
        }

        public static Contacto ExtraerContacto(string linea)
        {
            var contacto = linea.Split('|');
            return new Contacto
            {
                Nombre = contacto[0],
                Apellido = contacto[1],
                Telefono = contacto[2],
                Localidad = contacto[3],
                Codigo = contacto[4]
            };

        }

    }
}
