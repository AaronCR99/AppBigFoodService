using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using BLL;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace BLL
{
    public class Email
    {
        public void enviar(List<Factura> lstFactura, List<Detalle_Factura> lstDetalle_Facturas, string emailCliente, string nombreCliente)
        {

            try
            {
                var doc = new Document();
                MemoryStream memoryStream = new MemoryStream();
                PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);

                foreach (var item in lstFactura)
                {
                    doc.Open();
                    doc.Add(new Paragraph(
                        "Bienvenidos a Big Food Services.SA gracias por formar parte de nosotros\n \n" +
                        "\nA continuación se muestran los detalles de su compra: \n" +
                        "Nombre: " + nombreCliente +
                        "\nCedula: " + item.codCliente+ 
                        "\nEmail: " + emailCliente+
                        "\nProductos: "
                        ));
                    foreach (var item2 in lstDetalle_Facturas)
                    {
                        doc.Add(new Paragraph("\nProducto: " + item2.nombreProducto +
                            "\nCantidad: "+ item2.cantidad+
                            "\nSubtotal: ¢"+ item2.precioUnitario +
                            "\nTotal por producto: ¢" + item2.subTotal+
                            "\nImpuesto: ¢" + item2.porImp +
                            "\nDescuento: ¢"+ item2.porDescuento                            
                            ));
                    }

                    doc.Add(new Paragraph("\nSubtotal: ¢"+item.subTotal+
                        "\nMonto a cancelar: ¢" + item.total));
                    
                }
                writer.CloseStream = false;
                doc.Close();
                memoryStream.Position = 0;



                //Se crea una instancia del objeto email
                MailMessage email = new MailMessage();

                //agregamos los destinatarios
                //email del administrador para que reciba una copia
                email.To.Add(new MailAddress("proyecto.ucrnba@gmail.com"));

                //se agrega el email del usuario
                email.To.Add(new MailAddress(emailCliente));

                //se agrega el emisor
                email.From = new MailAddress("proyecto.ucrnba@gmail.com");

                //asunto email 
                email.Subject = "Compra de productos en Big Food Services.SA";

                ////se construye la vista  Html del body del email
                string html = "Bienvenidos a Big Food Services.SA gracias por formar parte de nosotros";

                email.IsBodyHtml = true;

                //se indica la prioridad del email
                email.Priority = MailPriority.Normal;

                email.Attachments.Add(new Attachment(memoryStream, "FacturaCompra.pdf"));

                //se crea la instancia para la vista html del body del email
                AlternateView view = AlternateView.CreateAlternateViewFromString(html,
                    Encoding.UTF8, MediaTypeNames.Text.Html);

                //se agrega la vista al email
                email.AlternateViews.Add(view);

                //se instancia un objeto SmtpCliente
                SmtpClient smtp = new SmtpClient();

                //se indica el servidor de correo a  implementar 
                smtp.Host = "smtp.gmail.com";

                //el puerto de comunicación
                smtp.Port = 587;

                //se indica si utiliza seguridad ssl
                smtp.EnableSsl = true;

                //se indica si tenemos credenciales por default
                //en este caso no 
                smtp.Credentials = new NetworkCredential("proyecto.ucrnba@gmail.com", "ApiUcr2021");

                //se envia el email
                smtp.Send(email);

                //se liberan los recursos 
                email.Dispose();
                smtp.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
