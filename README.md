# Rise

REQUIREMENTS:

* RABBITMQ
* MONGODB
* .NET CORE

You must run three projects at the same time(Rise Contact Api,Rise Report Api ve ReportBgService Worker)

You can adding,removing add updating a person with PersonApi.The same time, can add new features.For example; location, phone and email information.

The ReportApi can start a report request, change status the report.

The Background Worker handles RabbitMq queues, changes status and send email to the user who started the process.
