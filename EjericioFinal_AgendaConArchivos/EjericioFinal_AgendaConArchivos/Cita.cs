using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EjericioFinal_AgendaConArchivos
{
    class Cita 
    {
        //nombre de objetos
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }

        //variables locales
        static string fecha;
        static string hora;
        static string descripcion;
        static string nombre;
        static string codigo;
        static string opcion;
        static string guardar;
                public static void AgregarCita()
        {

            if (Contacto.ContactoPorNombre())
            {
                Console.WriteLine("Por favor ingresar los datos");

                Console.WriteLine("Codigo del Contacto");
                codigo = Console.ReadLine();
                Console.WriteLine("Fecha de la Cita en formato dia/mes/año");
                fecha = Console.ReadLine();
                Console.WriteLine("Hora en formato hora:minuto");
                hora = Console.ReadLine();
                Console.WriteLine("Descripcion");
                descripcion = Console.ReadLine();
                

                Console.WriteLine("\nDatos ingresados ");
                Console.WriteLine("Fecha: " + fecha);
                Console.WriteLine("Hora: " + hora);
                Console.WriteLine("Descripcion: " + descripcion);
                Console.WriteLine("Codigo de Contacto: " + codigo);


                Console.WriteLine("\nGuardar datos");
                Console.WriteLine("Presionar S para Guardar");
                Console.WriteLine("Presionar N para cancelar");
                guardar = Console.ReadLine();
                guardar = guardar.ToUpper();

                if (guardar == "S")
                {
                    FileStream crearAbrir = new FileStream("agenda.txt", FileMode.OpenOrCreate);
                    crearAbrir.Close();

                    FileStream agregar = new FileStream("agenda.txt", FileMode.Append);
                    string agenda = $"{fecha}|{hora}|{descripcion}|{codigo};";
                    agregar.Write(ASCIIEncoding.ASCII.GetBytes(agenda), 0, agenda.Length);
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
            else
            {
                Console.WriteLine("Agregar Contacto?\nPor favor ingresar el numero deseado \n1. Si\n2. No");
                opcion = Console.ReadLine();
                if(opcion == "1")
                {
                    Contacto.AgregarContacto();
                }
    
            }
        }

        public static void ConsultarCita()
        {
      
            Console.Clear();
            Console.WriteLine("***************************************");
            Console.WriteLine("**          Consultar cita           **");
            Console.WriteLine("***************************************");
            Console.WriteLine("** 1. Por fecha                      **");
            Console.WriteLine("** 2. Por contacto                   **");
            Console.WriteLine("***************************************");
            opcion = Console.ReadLine();
            if (opcion == "1")
            {
                Console.WriteLine("Citas por fecha");
                CitaPorFecha();
            }
            else if(opcion == "2")
            {
                if (Contacto.ContactoPorNombre())
                {
                    CitaPorCodigo();
                }
                    
            }
        }



        public static void CitaPorFecha()
        {
            bool comprobacion= false;
            int numeroCita = 1;
            Console.WriteLine("Ingresar la fecha en formato dia/mes/año");
            fecha = Console.ReadLine();
            Console.WriteLine("\n");

            byte[] infoCita = new byte[500000];
            FileStream abir = new FileStream("agenda.txt", FileMode.Open);
            abir.Read(infoCita, 0, (int)abir.Length);
            abir.Close();

            var datos = ASCIIEncoding.ASCII.GetString(infoCita);
            var citas = datos.Split(';').ToList();

            List<string> citasList = new List<string>();

            citas.RemoveAt(citas.Count - 1);
            foreach (var item in citas)
            {
                var cita = ExtraerCita(item);

                if (fecha == cita.Fecha)
                {
                    Console.WriteLine($"Datos de la Cita: { numeroCita}");
                    //Console.WriteLine($"Fecha: {cita.Fecha}");
                    Console.WriteLine($"Descripcion: {cita.Descripcion}");
                    Console.WriteLine($"Hora: {cita.Hora}");
                    Console.WriteLine($"Codigo contacto: {cita.Codigo}");
                    Contacto.ContactoPorCodigoSimple(cita.Codigo);

                    numeroCita++;
                    comprobacion = true;
                }

            }
            if (!comprobacion)
            {
                Console.WriteLine("No se encontro ninguna cita con la fecha ingresada ingresda");
            }
        }



        public static void CitaPorCodigo()
        {
            int numeroCita = 1;
            Console.WriteLine("Ingresar el codigo del contacto para ver sus citas");
            codigo = Console.ReadLine();

            byte[] infoCita = new byte[500000];
            FileStream abir = new FileStream("agenda.txt", FileMode.Open);
            abir.Read(infoCita, 0, (int)abir.Length);
            abir.Close();

            var datos = ASCIIEncoding.ASCII.GetString(infoCita);
            var citas = datos.Split(';').ToList();

            List<string> citasList = new List<string>();

            citas.RemoveAt(citas.Count - 1);
            foreach (var item in citas)
            {
                var cita = ExtraerCita(item);

                if (codigo == cita.Codigo)
                {
                    citasList.Add
                        (
                        $"\nDatos de la Cita: { numeroCita}" +
                        $"\nFecha: {cita.Fecha}" +
                        $"\nHora: {cita.Hora}" +
                        $"\nDescripcion: {cita.Descripcion}"
                        );
                        
                numeroCita++;
                }

            }

            if (citasList.Any())
            {
                Console.Clear();
                Console.WriteLine("Datos del contacto");
                Contacto.ContactoPorCodigo(codigo);
                foreach (var item in citasList)
                {
                    Console.WriteLine(item);
                }
            }

            else
            {
                Console.WriteLine("No se encontro ninguna cita con el codigo ingresado");
            }
        }

        public static Cita ExtraerCita(string linea)
        {
            var cita = linea.Split('|');

            
                return new Cita
                {
                    Fecha = cita[0],
                    Hora = cita[1],
                    Descripcion = cita[2],
                    Codigo = cita[3]
                };
           
        }
    }
}
