# SampleCode_Server
Sample code: The core bounded context of the macromanager desktop application. 
* Note that this code will not compile because some files were excluded to protect sensitive information.

### Global Architecture
MacroManager is a distributed application with a backend based on Domain Driven Design. The backend is composed of an API gateway that communicates with loosely coupled microservices. Each microservice is a full, but miniature application that has a specialized scope of responsibility, including authentication/authorization and the core service. 

### Code Sample
The code sample I provided is the core microservice. The code is based on n-tier/onion architecture and it contains the following layers: Domain, Persistence, ApplicationServices, and Infrastructure. 
* The infrastructure layer is the outermost layer and it contains much of the configuration, including registrations for dependency injection as well as abstractions that wrap 3rd party dependencies like the ORM (entity framework).
* The ApplicationServices layer houses the business logic, which is based on the Command Query Separation design pattern. The logic is organized into query or command handlers depending on whether the functionality is for reading or writing, respectively. For more information on the benefits of this design pattern you can check out these articles [Commands](https://blogs.cuttingedge.it/steven/posts/2011/meanwhile-on-the-command-side-of-my-architecture/) and [Queries](https://blogs.cuttingedge.it/steven/posts/2011/meanwhile-on-the-query-side-of-my-architecture/).
