# Rise

REQUIREMENTS:

* RABBITMQ
* MONGODB
* .NET CORE

You must run three projects at the same time(Rise Contact Api,Rise Report Api ve ReportBgService Worker)

You can adding,removing add updating a person with PersonApi.The same time, can add new features.For example; location, phone and email information.
![image](https://user-images.githubusercontent.com/39440721/161424597-5b8e1b6f-e21c-4b5a-8df2-3b37e085b4d6.png)

The ReportApi can start a report request, change status the report.
![image](https://user-images.githubusercontent.com/39440721/161424583-2519da6b-943e-4a1b-b982-d440a5b26da8.png)

The Background Worker handles RabbitMq queues, changes status and send email to the user who started the process.
![image](https://user-images.githubusercontent.com/39440721/161424604-f5e78d9c-5939-4d2d-9a1f-e43157dc6436.png)
