#Config Database (Code first):
In appsetigs.json cofigure your ConnectionStrings (use empty Database, EF code first will create tabes)
or create a new SQL Server DB with name : Testdb.db.dev

#Rus Migration to generate SQL DB Scripte and create DB table
on the Visual Stidio 2022 open terminal an run : 
->  cd envato\DotNet.SkillTestBack\SkillTest.API.UI
->  dotnet ef --project ..\SkillTest.Core.Infrastructures\ migrations add MigrationName Initial --context SkillTestDBContext
->  dotnet ef  database update

#Start App
Generate SkillTest.API.UI
run IN ISS Express

#
you can acces to swagger on : https://localhost:7146/swagger/index.html
on swagger interface :
# use register API to create a new user 
# use logoi Api to get access token
# copy the token and put him on autorizations 
-> Clique on autorizations button and on value text zone use the Bearer scheme: bearer copied_Token 
# you can now use get post and put api
