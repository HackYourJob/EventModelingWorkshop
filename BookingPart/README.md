CQRS ES workshop - Hotel exercice

To build the app, 
1 - build the different modules and publish the docker image by doing a "gradlew build" in the project root folder.
2 - building the docker image and publishing it is based on jib. For more information, see the documentation at this url: 
https://github.com/GoogleContainerTools/jib/

you can start the rest exposition by executing the following command line in the exposition folder:  
**mvn  spring-boot:run**  

The url to use the service is the following:  
**http://<host>:<port>/hotel/swagger-ui.html**

Some parameters have or can be configured in the application.properties file in the exposition project (like the application port for example)
