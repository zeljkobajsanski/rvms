Web.config
----------
<add name="KorisniciConnection" providerName="System.Data.SqlClient" connectionString="Server=tcp:ahg2g9tnre.database.windows.net,1433;Database=korisnici;User ID=zeks@ahg2g9tnre;Password=Z3ks_J0va;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;" />
<forms loginUrl="http://korisnici.azurewebsites.net/Account/Login?appCode=rvms" timeout="2880"  name="auth_cookie"/>

_Layout2.cshtml
---------------
<li><a href="http://korisnici.azurewebsites.net/account/mojprofil">Moj nalog</a></li>