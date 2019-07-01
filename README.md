# Cube Summation HackerRank's Challenge
Esta aplicación resuelve el reto propuesto por HackerRank -> "Cube Summation": https://www.hackerrank.com/challenges/cube-summation/problem

### Las capas de la aplicación (por ejemplo, capa de persistencia, vista, de aplicación, etc) y qué clases pertenecen a cuál.

#### Capa de Presentación
Está comprendido por la interfaz gráfica web que realicé en un proyecto angular, este contiene: los componentes, encargados de la interacción del usuario con el formulario, los modelos, son clases que se utilizan para mantener la estructura de POO y los servicios, son los encargados de realizar las peticiones a los endpoint del WebApi. Cada uno de estos es una carpeta dentro del src/app.

#### Capa de Reglas de Negocio
Esta la llamé BR (Business Rules), es la encargada de aplicar todas las reglas de la aplicación, contiene las clases: Factory, ValidacionDatos, FuncionesGenericas, se encuentran las carpetas BrClasses y BrInterfaces, dentro de la primera están las clases: CubeSummationBR y OperacionBR, dentro de la segunda están las interfaces: ICubeSummationBR y IOperacionBR.

#### Capa de Acceso a Datos
Esta la llamé DAL (Data Access Layer), es la encarga de centralizar el manejo hacia el origen de datos, pero como para este proyecto no fue necesario dejé a propósito el proyecto vacío.

#### Capa de Modelos
Esta contiene todos los modelos y/o elementos que se necesiten a nivel de aplicación, y las entidades que vienen del origen de datos. Para este caso dentro de la carpeta ‘Models’ situé las clases: Cubo, OperacionCubo y RespuestaGeneral. Dentro de la carpeta ‘Resources’ se encuentra CubeSummationResources.resx y por último, una enumeración llamada TipoOperacion situada en la carpeta ‘Enumerables’

#### Capa Vista (WebApi)
Este contiene los controladores con los métodos que son expuestos, en este caso hay dos controladores: BaseController y CubeSummationController.

#### WebApi --> Referencia al BR y Model
#### BR -->  Referencia al DAL y Model
#### DAL --> Referencia al Model
#### Model --> No referencia a capas superiores. Esta es la capa más baja.

### La responsabilidad de cada clase creada.

#### Proyecto BR
##### Clase Factory
Tiene la responsabilidad de instanciar todas las clases que contienen reglas de negocio (xBR), es llamado por los controller y este les devuelve la instancia de la clase solicitada para que la pueda usar. También aquí se aplican todas las configuraciones y/o inyección de dependencias que sean necesarias para cada clase.

##### Interfaz IOperacionBR y clase OperacionBR
La interfaz tiene la responsabilidad de definir el contrato que va a usar su clase homogénea, se ubican los métodos que son expuestos. La clase, tiene la responsabilidad de heredar de su interfaz e implementar su contrato, adicionalmente es responsable de realizar todo el proceso necesario por cada tipo de operación (QUERY, UPDATE).

##### Interfaz ICubeSummationBR y clase CubeSummationBR
Tiene la responsabilidad de heredar de su interfaz homogénea e implementar su contrato, adicionalmente se encarga de iniciar todo el procesamiento de la información que llega de la web.

##### Clase ValidacionDatos
Tiene la responsabilidad de alojar todas las validaciones de datos que sean necesarias en el momento de mapear la información que llega de la web a los modelos que se manejan internamente. Esta clase es utiliza por CubeSummationBR.

##### Clase estática FuncionesGenericas
Tiene la responsabilidad de mantener todos aquellos métodos que son generales, principalmente para evitar el código duplicado.

#### Proyecto Model
##### Clase Cubo, OperacionCubo
Tiene la responsabilidad de definir las propiedades que se tendrán para el manejo de los datos que llegan de la web y una vez sean mapeados a estas.

##### Clase RespuestaGeneral
Tiene la responsabilidad de definir las propiedades que tendrá la respuesta. Todas las respuestas hacia el front tienen esta estructura.

##### Enumeracion TipoOperacion
Tiene la responsabilidad de definir los tipos de operación que se manejan para la aplicación (QUERY, UPDATE).

##### Recurso CubeSummationResources.resx
Tiene responsabilidad de contener todos los mensajes que se utilizan a nivel de la aplicación y se evita la mala práctica del HardCode.


##### Proyecto WebApi
Adicional a las clases que se crean automáticamente para un proyecto de este tipo (Global.asax, WebApiConfig, WebConfig, etc), se encuentran:

##### Controller BaseController
Tiene la responsabilidad de heredar de ApiController, para que todos los demás hereden de este y situar allí todos los métodos que son generales para la capa de vista, como creación de respuestas, manejo de posibles excepciones, etc.

##### Controller CubeSummation
Tiene la responsabilidad de exponer los métodos endpoint, de ser el puente entre cada petición y la capa de negocio mediante la clase Factory que se explicó anteriormente y de devolver una respuesta general. (Recibo – Transporto – Respondo).

##### Clase TextMediaTypeFormatter
Tiene la responsabilidad de manejar las peticiones de tipo “text/plain” para que desde el WebApiConfig se adicione esta configuración.

#### Mecanismo usado para la entrada y salida de datos es Web

Decidí crear un proyecto totalmente externo a la solución de .Net, en este caso Angular, con la premisa de que: Si se quiere cambiar la interfaz de la aplicación a otro lenguaje, sistema, etc, todo es externo a la solución construida, en este caso .net, permitiendo independencia y control de cambios. 
Se encuentra en Fuentes/Front-End/

##### ¿En qué consiste el principio de responsabilidad única?
Es el primero de los principios SOLID, consiste en que cada objeto/clase que se construya tenga bajo su poder una sola responsabilidad encapsulada por clase, esto permite que cuando se quieran hacer cambios de una funcionalidad x, solo se realizan allí y las demás clases continúan siendo las mismas. Permitiendo facilidad de lectura, mejor mantenibilidad, código de complejidad menor y bajo acoplamiento.

##### ¿Qué es código limpio?
El código limpio se refiere al estilo de desarrollo de tal manera que se escriba un código que sea legible, entendible por otra persona. Un código limpio, es aquel es que fácil de leer y mantener.
Personalmente me gusta mucho esta frase: “Cualquier estúpido puede escribir un código que los ordenadores puedan entender. Son los buenos programadores los que escriben códigos que los humanos pueden entender”. (Martin Fowler).
