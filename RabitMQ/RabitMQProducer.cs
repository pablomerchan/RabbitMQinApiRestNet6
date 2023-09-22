using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
namespace RabitMqProductAPI.RabitMQ
{
    public class RabitMQProducer : IRabitMQProducer
    {
        public void SendProductMessage<T>(T message)
        {
            //Aquí especificamos el servidor Rabbit MQ. Usamos la imagen acoplable de Rabbitmq y la usamos.
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            //Crea la conexión RabbitMQ usando los detalles en las conexiones.
            var connection = factory.CreateConnection();
            //Aquí creamos canal con sesión y el modelo
            using
            var channel = connection.CreateModel();

            //declara la cola después con el nombre y algunas propiedades relacionadas a el
            channel.QueueDeclare("product", exclusive: false);
            //serializar el mensaje
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            //coloca los datos en la cola de productos
            channel.BasicPublish(exchange: "", routingKey: "product", body: body);

            Console.WriteLine($" [x] Sent {message}");
        }
    }
}