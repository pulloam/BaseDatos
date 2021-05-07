using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;

namespace TrabajoB {
    class Program {
        static void Main(string[] args) {
            //BD.Conectar();

            LeerLibroExcel();

            
            Console.ReadKey();
        }

        private static void LeerLibroExcel(){ 
            Excel.Application excelApp = null;
            Excel.Workbooks xlLos_Libros = null;

            Excel.Workbook xlLibro = null;
            Excel.Worksheet xlHoja = null;

            try {
                excelApp = new Excel.Application();
                xlLos_Libros = excelApp.Workbooks;

                xlLibro = xlLos_Libros.Add(@"C:\Users\Personal\OneDrive\Escritorio\Clientes.xlsx");
                xlHoja = xlLibro.Sheets[1];

                Excel.Range xlRango = xlHoja.UsedRange;

                for (int i = 1; i <= xlRango.Rows.Count; i++) {
                    Console.WriteLine("");
                    for (int j = 1; j <= xlRango.Columns.Count; j++) {
                        if (xlRango.Cells[i, j] != null && xlRango.Cells[i, j].Value2 != null) {
                            Console.Write(xlRango.Cells[i, j].Value.ToString() + "\t");

                        }
                    }
                }
            } catch (Exception ex) {
                Debug.WriteLine("Error -> " + ex.ToString());
            } finally{ 
                //Liberar recursos
                if(xlLibro != null) xlLibro.Close(false, null, null);

                if(xlLos_Libros != null) xlLos_Libros.Close();
                if(excelApp != null) excelApp.Quit();

                if(xlHoja != null) Marshal.ReleaseComObject(xlHoja);
                if(xlLibro != null) Marshal.ReleaseComObject(xlLibro);
                if(xlLos_Libros != null) Marshal.ReleaseComObject(xlLos_Libros);
                if(excelApp != null) Marshal.ReleaseComObject(excelApp);
            }
        }

    }
}
