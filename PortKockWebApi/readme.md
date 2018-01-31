# Routes

request allowance or rejection
	request:
		POST -d '{action:"allow", ip:"127.0.0.1", port:8080}' /api/portkock
		POST -d '{action:"deny", ip:"127.0.0.1", port:8080}' /api/portkock

	response:
		when OKay, {"result":"OK"}
		when NG, {"result":"NG", "message":"bla bla ..."}
	
# to-do

- proper listing feature current opened destinations

	list opened destination
	request:
		GET /api/portkock[?items=50&page=1]
	response:
		{items:[
			{id="", requested_at="", expired_at="", destination={action:"allow", ip:"127.0.0.1", port:8080}, result:{}},
			...
		]}

- proper getting request specific item details feature
	request:
		GET /api/portkock/<id>
	response:
		...

- proper logging feature
- proper persistent to records
- proper scheduling to automated expiration
- not to show IIS error page

# Pakcages(dependencies)

Install-Package log4net -Version 2.0.8
Install-Package Quartz -Version 3.0.2

