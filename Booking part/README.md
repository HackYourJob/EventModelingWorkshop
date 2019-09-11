CQRS ES workshop - Hotel exercice

To build the app, 
1 - build the different modules by doing a "mvn install" in the project root folder.
2 - build the docker image and publish it. To do so execute the following command in the exposition folder: "mvn compile jib:build"
Be sure to have configure in your maven settings.xml the server of your docker registry.
For more information, see the documentation at this url: 
https://github.com/GoogleContainerTools/jib/tree/master/jib-maven-plugin#using-maven-settings

you can start the rest exposition by executing the following command line in the exposition folder:  
**mvn  spring-boot:run**  

The url to use the service is the following:  
**http://<host>:<port>/hotel/swagger-ui.html**
