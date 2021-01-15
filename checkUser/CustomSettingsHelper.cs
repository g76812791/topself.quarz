using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    class CustomSettingsHelper
    {
        public static TrendsEnvironment IsDevEnviroment()
        {
            int environmentValue = 0;
            try
            {
                var section = ConfigurationManager.GetSection("customSettings") as NameValueCollection;
                var value = section["IsDevEnviroment"];
                int.TryParse(value, out environmentValue);
                if (environmentValue > 2)
                {
                    environmentValue = 1;
                }
            }
            catch (Exception) { }
            return (TrendsEnvironment)environmentValue;
        }

        public static string GetCustomSetting(string settingName)
        {
            try
            {
                var section = ConfigurationManager.GetSection("customSettings") as NameValueCollection;
                var value = section[settingName];
                return value == null ? string.Empty : value.ToString();
            }
            catch (Exception) {
                return string.Empty;
            }
        }



        public static string GetConnectionStringName(string name)
        {
            switch (IsDevEnviroment())
            {
                case TrendsEnvironment.Staging:
                    return name + "Staging";
                case TrendsEnvironment.Dev:
                    return name + "Dev";
                case TrendsEnvironment.Prod:
                    return name;
                default:
                    return name + "Dev";
            }
        }

        public static string GetStorageName(string name)
        {
            switch (IsDevEnviroment())
            {
                case TrendsEnvironment.Staging:
                    if (name.IndexOf("flashfood") == 0)
                        return "flashfoodstftaging";
                    else if (name.IndexOf("3xapp") == 0)
                        return "3xappservicestaging";
                    else if (name.IndexOf("3xcentralmonitor") == 0)
                        return "3xcentralmonitorstaging";
                    return name + "staging";
                case TrendsEnvironment.Dev:
                    if (name.IndexOf("3xposdata") == 0)
                        return "3xposdatadev2";
                    else if (name.IndexOf("3xtrends") == 0)
                        return "3xtrendsdev2";
                    return name + "dev";
                case TrendsEnvironment.Prod:
                    return name;
                default:
                    return name;
            }
        }

        public static string GetCosmosContainerName()
        {
            switch (IsDevEnviroment())
            {
                case TrendsEnvironment.Staging:
                    return "classificationsmonitoringdev";
                case TrendsEnvironment.Dev:
                    return "classificationsmonitoringdev";
                case TrendsEnvironment.Prod:
                    return "classificationsmonitoringcirclek";
                default:
                    return string.Empty;
            }
        }

        public static string GetNonValidCosmosContainerName()
        {
            switch (IsDevEnviroment())
            {
                case TrendsEnvironment.Staging:
                    return "classificationsmonitoringnonvaliddev";
                case TrendsEnvironment.Dev:
                    return "classificationsmonitoringnonvaliddev";
                case TrendsEnvironment.Prod:
                    return "classificationsmonitoringnonvalidcirclek";
                default:
                    return string.Empty;
            }
        }

        public static string GetCosmosDbName()
        {
            switch (IsDevEnviroment())
            {
                case TrendsEnvironment.Staging:
                    return "classificationsmonitoringdev";
                case TrendsEnvironment.Dev:
                    return "classificationsmonitoringdev";
                case TrendsEnvironment.Prod:
                    return "classificationsmonitoring";
                default:
                    return string.Empty;
            }
        }

        public static string GetStorageConnectionString(StorageKeys storageName, int environmentValue = -1)
        {
            var trendsEnvironment = IsDevEnviroment();

            if (environmentValue >= 0)
            {
                trendsEnvironment = (TrendsEnvironment)environmentValue;
            }

            switch (trendsEnvironment)
            {
                case TrendsEnvironment.Staging:
                    if (StagingStorageDicts.ContainsKey(storageName))
                    {
                        return StagingStorageDicts[storageName];
                    }
                    break;
                case TrendsEnvironment.Dev:
                    if (DevStorageDicts.ContainsKey(storageName))
                    {
                        return DevStorageDicts[storageName];
                    }
                    break;
                case TrendsEnvironment.Prod:
                    if (ProdStorageDicts.ContainsKey(storageName))
                    {
                        return ProdStorageDicts[storageName];
                    }
                    break;
            }
            return string.Empty;
        }

        public static string GetDocumentDbConnectionString(DocumentDBKeys documentDbName)
        {
            var trendsEnvironment = IsDevEnviroment();

            switch (trendsEnvironment)
            {
                case TrendsEnvironment.Staging:
                    if (StagingDocDBDicts.ContainsKey(documentDbName))
                    {
                        return StagingDocDBDicts[documentDbName];
                    }
                    break;
                case TrendsEnvironment.Dev:
                    if (DevDocDBDicts.ContainsKey(documentDbName))
                    {
                        return DevDocDBDicts[documentDbName];
                    }
                    break;
                case TrendsEnvironment.Prod:
                    if (ProdDocDBDicts.ContainsKey(documentDbName))
                    {
                        return ProdDocDBDicts[documentDbName];
                    }
                    break;
            }
            return string.Empty;
        }

        public static string GetDBConnectionString(DatabaseKeys dbKeys)
        {
            var trendsEnvironment = IsDevEnviroment();

            switch (trendsEnvironment)
            {
                case TrendsEnvironment.Staging:
                    if (StagingDBDicts.ContainsKey(dbKeys))
                    {
                        return StagingDBDicts[dbKeys];
                    }
                    break;
                case TrendsEnvironment.Dev:
                    if (DevDBDicts.ContainsKey(dbKeys))
                    {
                        return DevDBDicts[dbKeys];
                    }
                    break;
                case TrendsEnvironment.Prod:
                    if (ProdDBDicts.ContainsKey(dbKeys))
                    {
                        return ProdDBDicts[dbKeys];
                    }
                    break;
            }
            return string.Empty;
        }

        private static Dictionary<StorageKeys, string> ProdStorageDicts = new Dictionary<StorageKeys, string> {
            { StorageKeys.accesscontrol,"DefaultEndpointsProtocol=https;AccountName=3xaccesscontrol;AccountKey=PBVE6nqUavCB8kvFWxJdImgV2eEYQoBEp6en8iLj8Hm0gtvUWDwcbZtCveHXRPYlWNaKPlZ2teXTvp6zDhsdlQ==;EndpointSuffix=core.windows.net"},
            { StorageKeys.alarms,"DefaultEndpointsProtocol=https;AccountName=3xalarms;AccountKey=t7Myv010UcMiF2ot3yQ6Aj8EngaNLtIwlJAdGmCfb98L9ifGQcyOMsxcaEj3UwfpULK5VMd36owG2qXBO+y+ng=="},
            { StorageKeys.analytics,"DefaultEndpointsProtocol=https;AccountName=3xanalytics;AccountKey=VAYwOmphTQCyf8E3n6j8v4awbaR2Mm0X3RArNJqXG8lVwvK+17ovpnLjmBqIb3kKbbxpIWIOmW0LlLwnE56+QA==;EndpointSuffix=core.windows.net"},
            { StorageKeys.appservice,"DefaultEndpointsProtocol=https;AccountName=3xapplicationservice;AccountKey=7t+Zo793KKF96+KVN6+ywNGPgNZRL0rNYcoNp2RD32rGkVvj1OHxF+uxAkBEfZdwoO5xxfJ/U7fnNecsFmTTsg=="},
            { StorageKeys.audit,"DefaultEndpointsProtocol=https;AccountName=3xaudit;AccountKey=MpPBavfMtNudNHYdsGHbuOm/Z/FnRdnlddtxDwVE4POyO9VH25fd68zISM+OSddJH8meZolQqm/Rbw/zK0Y0Xg==;EndpointSuffix=core.windows.net"},
            { StorageKeys.cache,"DefaultEndpointsProtocol=https;AccountName=3xcache;AccountKey=wm1V6F485yeS8koqOmDjAf0v9eRnWLGwlArr+BVlvkPjWT73Z6AzZvPhfSXm+a9vdMTVbemE+vQDxmv8n7mQWg==;EndpointSuffix=core.windows.net"},
            { StorageKeys.centralmonitoring,"DefaultEndpointsProtocol=https;AccountName=3xcentralmonitoring;AccountKey=/vsZHfEICshh0Mse1TXyLziAajT1HYKB9xZ3La7ev8VsYxTM3x8wlRbpZTRB/uJFxLdV3yPxRU7SQmnz+BCzNw=="},
            { StorageKeys.email,"DefaultEndpointsProtocol=https;AccountName=3xemail;AccountKey=wsznM874+OpqSYOHnQPAKhi3Wi7iIsY1vVBvgPHo88JxlGASz7n/I2W+A2zJidQP85vHFBoJ4LTMX101EWksUw==;EndpointSuffix=core.windows.net"},
            { StorageKeys.posdata,"DefaultEndpointsProtocol=https;AccountName=3xposdata;AccountKey=PasrU3uTiWvxde1ujFhnAT9yqaPkFFyEdlFmhORAXLyQAQRQIMhJwS1K6Ood/08NV3SnyzdHCDJHdY+lOPoLKw==;EndpointSuffix=core.windows.net"},
            { StorageKeys.queue,"DefaultEndpointsProtocol=https;AccountName=3xqueue;AccountKey=yYIZBpG/Fm0oKZ9Lw/2109A+OkRydUFYX3ufd2Nme9xBSXIV45s2E9AHRVtEuH9256QDpSRgR+bN/22nMn7cPA==;EndpointSuffix=core.windows.net"},
            { StorageKeys.store,"DefaultEndpointsProtocol=https;AccountName=3xstore;AccountKey=JlhEc95Vn4AgQDvIGq/ChSbfagxi37a6SrbMk9jyHDJ71XdfmAqax+4inpC8KFnxU8ZxmdQrtuxiAWYmamRdMw==;EndpointSuffix=core.windows.net"},
            { StorageKeys.trends,"DefaultEndpointsProtocol=https;AccountName=3xtrends;AccountKey=ZYRrl/vOxYozt/JpxPwP0ulye3CghJ5KNTqgVi49M5tP14ffohdGs/zpGTVpD8HQZMDg/7K8mJCPiOCihFIBIA=="},
            { StorageKeys.upload,"DefaultEndpointsProtocol=https;AccountName=3xupload;AccountKey=OmpD13YPIM9XPCu54EqD/BZ8VRhtog3EFfeX1hxSH2nhVXW/KMptxnLotNOlzQSn0RHEZ4BafIzbnDgfU7wC4Q==;EndpointSuffix=core.windows.net"},
            { StorageKeys.videojobs,"DefaultEndpointsProtocol=https;AccountName=3xvideojobs;AccountKey=nTjX5E1SShQdj4g5vaJE1xvsswPwuXhoXNl51THu6jsIJM3Zd/Vs6jFm4KeFx03TxL3t0P7SEcMbeooITyU7vQ==;EndpointSuffix=core.windows.net"},
            { StorageKeys.flashfoodstransfer,"DefaultEndpointsProtocol=https;AccountName=flashfoodstransfer;AccountKey=VVAlWwyAO+F+Zl+vaFcx/ZLyixprruLos3vLh7d7qzrKNblAbpuR1UF4TsfBQwdSevs/R0FFFgYzU7tj+6p7Jw==;EndpointSuffix=core.windows.net"},
            { StorageKeys.pandatransfer,"DefaultEndpointsProtocol=https;AccountName=pandatransfer;AccountKey=70F0Gg7Ve3GVVHjFUnNhGSXsTLGfjNi90JUm3AqlO719lQik/bXUxuS02rRiS21El4lKan+u3VtvZa/jgNARFA==;EndpointSuffix=core.windows.net"},
            { StorageKeys.biglotstransfer,"DefaultEndpointsProtocol=https;AccountName=biglotstransfer;AccountKey=y4Hv6t1lg4SXgDqcNrMO8sajc77taIwUkDxbLcObUjqQYUkS2fZAk5lY14cmm9Ut1jc21ml3YQdXN6N5Twolbg==;EndpointSuffix=core.windows.net"},
             { StorageKeys.rubiostransfer,"DefaultEndpointsProtocol=https;AccountName=rubiostransfer;AccountKey=DhB7xbnoDssY9Dfjq7TvUPSHnjQnxL8Iksw6Q7Y7xm4FAZBO/fjEb4VNRFS40vmUp2hhD6NJ3SmJOV9/28gaOw==;EndpointSuffix=core.windows.net" },
            { StorageKeys.circlekposdata,"DefaultEndpointsProtocol=https;AccountName=circlekposdata;AccountKey=4pyDpSfkp/U7P5w3q5EAFjU4T1a8FLGb0WFLWSsLt65Je7Ixy+2bjxsKaHbAYd5CGPznnF4+TCVsz4spDLrOvQ==;EndpointSuffix=core.windows.net"},
            { StorageKeys.vdcstorage,"DefaultEndpointsProtocol=https;AccountName=vdcdatastorage;AccountKey=WjDcTOPd3StY913IJ8RBT5zGU0kVnm0J8es2J5iRApPwUOSqn5fB52V1kmbT2JsGM89YPwN9bu/5Fa1E3EBlOA==;EndpointSuffix=core.windows.net"},
            { StorageKeys.migrationservice,"DefaultEndpointsProtocol=https;AccountName=3xmigrationservice;AccountKey=yw8ioS9uJZ/Xvzcx03uwEi3QkTiUinlpH6r5vxROPFBqAd+xCuHG1fITtw7IDtAmCTqTMrXxd3xMiL+R7CFwkg==;EndpointSuffix=core.windows.net"}
        };

        private static Dictionary<StorageKeys, string> DevStorageDicts = new Dictionary<StorageKeys, string> {
            { StorageKeys.accesscontrol,"DefaultEndpointsProtocol=https;AccountName=3xaccesscontroldev;AccountKey=+X3yjmZNiyVkeCARgCzmh638lv5h8UbmR6TiGOdBpCUriRlVLW0AOV4T68Fsak6XuNnZhtxZhZQPn/Axnzo5rg==;EndpointSuffix=core.windows.net"},
            { StorageKeys.alarms,"DefaultEndpointsProtocol=https;AccountName=3xalarmsdev;AccountKey=Ev13EB8hcI42ABonGGbe320rt807TlsHNeM+JLKPHsyRmTw+yeDdpQlUmdgr2xdxUVJj3V9HVjsCC/EpjLewaQ==;EndpointSuffix=core.windows.net"},
            { StorageKeys.analytics,"DefaultEndpointsProtocol=https;AccountName=3xanalyticsdev;AccountKey=ljv24ElyINw6YnkDqdWPkYxczu4QMk6tCKUZ8isTCz0hIxU+/lWyglqE2NidXYdozw5CME1uyNGFzICB3BZdsA==;EndpointSuffix=core.windows.net"},
            { StorageKeys.appservice,"DefaultEndpointsProtocol=https;AccountName=3xapplicationservicedev;AccountKey=Z+QjCxOWMWRBTGa3ESAQ1U+WHEIUhbrRugUXhcqZOzxQdrQNHM0QUlMAbYn82vUr20MMieB/wyhx474guPoRRg==;EndpointSuffix=core.windows.net"},
            { StorageKeys.audit,"DefaultEndpointsProtocol=https;AccountName=3xauditdev;AccountKey=pxGcmkq9HzrfKuynKSMnLQBJ85S1WHK+l6gQsIwriTt3mX2xKa6TLkRAXwVVuWq8qOs89bNqbu3mIRus0S+2HQ==;EndpointSuffix=core.windows.net"},
            { StorageKeys.cache,"DefaultEndpointsProtocol=https;AccountName=3xcachedev;AccountKey=tqwBeiDS04Nil4nA8S5OWZH5+fqzOS9+v0iTlGPH8eZRGBjgo80fuW+XRJ4aszLLHCKGztPd719BVy65nuSB1g==;EndpointSuffix=core.windows.net"},
            { StorageKeys.centralmonitoring,"DefaultEndpointsProtocol=https;AccountName=3xcentralmonitoringdev;AccountKey=5nI/lI4KRZqNTZsZEbC4bGp8U2SWD43gzfBrNH2eFIPlVBVe/J7iL+fEOJm8ipyHmivoFMRX1mucbKxQPfl3Bg==;EndpointSuffix=core.windows.net"},
            { StorageKeys.email,"DefaultEndpointsProtocol=https;AccountName=3xemaildev;AccountKey=w/ui/8etDI4JLdVUaAp6o8RhT2ShBmNslbtauB7uqt9gMdXK/IPvcGxijtR+F+Q8ttkzu3dGjtRHQJgH+BTkgQ==;EndpointSuffix=core.windows.net"},
            { StorageKeys.posdata,"DefaultEndpointsProtocol=https;AccountName=3xposdatadev2;AccountKey=J+MKmc/CBbvE7YyuGkL89YPhtCNnXkaytMsQFIYSA9glFXxoYKV1cksoNqiEZLmQRwuRTcwP8pspe+GZzWqEqQ==;EndpointSuffix=core.windows.net"},
            { StorageKeys.queue,"DefaultEndpointsProtocol=https;AccountName=3xqueuedev;AccountKey=KkKYKYX3SQuYESZCMQ2asG1pjZItvXe8YO828AI/pYq0WniMComffG6TFmGm7oJqqWgUJWYmlnRQy9aDQxr2DA==;EndpointSuffix=core.windows.net"},
            { StorageKeys.store,"DefaultEndpointsProtocol=https;AccountName=3xstoredev;AccountKey=eXfvVBpMYU7TJC9JGt19WD3kKSvZ1CQq5lKW07qj5gu5NGEYN1pNkuoTedQyd42FcEXTYI0hF6mddIbpClchOw==;EndpointSuffix=core.windows.net"},
            { StorageKeys.trends,"DefaultEndpointsProtocol=https;AccountName=3xtrendsdev2;AccountKey=cZLoSudOMIxkz8uUKGaS/AONpGC0+YS8vuZ9G3jth4sWYWZ9KH7ae65FvPSkKEzmyvk4cSfXKeKbJcsxSrvc3Q==;EndpointSuffix=core.windows.net"},
            { StorageKeys.upload,"DefaultEndpointsProtocol=https;AccountName=3xuploaddev;AccountKey=iCoItKwK1yEbGFjMSLFIMxcjLcjammOiJilWd7aFU1e5k9WUKLXtXmU4iFoA+ZVx2wPDpRnK/XA6IjDMQteEmA==;EndpointSuffix=core.windows.net"},
            { StorageKeys.videojobs,"DefaultEndpointsProtocol=https;AccountName=3xvideojobsdev;AccountKey=G9nA6aePeIrqV9DCLoGujABDxoDFDp937GYXptM6jzjuffuAJgJfZ68K4nJSFgx/EEaNdeda9ISQtXaxXZDeCg==;EndpointSuffix=core.windows.net"},
            { StorageKeys.flashfoodstransfer,"DefaultEndpointsProtocol=https;AccountName=flashfoodstransferdev;AccountKey=0AE7IZka03dWyz5j0pOfRfkChcq/jiZKB8YDm0kD+1o6WmAyW85CosqEkqUq22NWIBZ7dvDl4GYTQcWGgGR+iQ==;EndpointSuffix=core.windows.net"},
            { StorageKeys.pandatransfer,"DefaultEndpointsProtocol=https;AccountName=pandatransferdev;AccountKey=HLj2/8bm3srFJDVw/UZ6R4T0qgnek55SObvUM8bYTEdDECuWKzHYMEE5DQbgpplUeZ5nDRPsDQmDuau4vdmKIg==;EndpointSuffix=core.windows.net"},
            { StorageKeys.biglotstransfer,"DefaultEndpointsProtocol=https;AccountName=biglotstransferdev;AccountKey=ri3ThAKqCeqR+3X5+TUdfWnQCzk6LWI4dlQ3DcfniO19eiqYdVMst6EEmjYDA6I2POt25lynZhzm6TRl1lKD+Q==;EndpointSuffix=core.windows.net"},
            { StorageKeys.rubiostransfer,"DefaultEndpointsProtocol=https;AccountName=rubiostransferdev;AccountKey=tjg0t0zv1VtGHy+X7CSc2uNeeSt8SQlJkE7kReNwTkAAhv3xR+2uPEA6myJDVJSkxSqPiXvGapkWjL+E1PqiyQ==;EndpointSuffix=core.windows.net" },
            { StorageKeys.circlekposdata,"DefaultEndpointsProtocol=https;AccountName=circlekposdata;AccountKey=4pyDpSfkp/U7P5w3q5EAFjU4T1a8FLGb0WFLWSsLt65Je7Ixy+2bjxsKaHbAYd5CGPznnF4+TCVsz4spDLrOvQ==;EndpointSuffix=core.windows.net"},
            { StorageKeys.vdcstorage,"DefaultEndpointsProtocol=https;AccountName=vdcdatastorage;AccountKey=WjDcTOPd3StY913IJ8RBT5zGU0kVnm0J8es2J5iRApPwUOSqn5fB52V1kmbT2JsGM89YPwN9bu/5Fa1E3EBlOA==;EndpointSuffix=core.windows.net"},
            { StorageKeys.migrationservice,"DefaultEndpointsProtocol=https;AccountName=3xmigrationservicedev;AccountKey=btHApQOuBhk4+5g1FTqTagu4RrO1E6+EhI0GQlXgD5RxishZviORhkDaw07ukq8zhFQo+A1JRSwgCnY/qBnRqw==;EndpointSuffix=core.windows.net"}
        };

        private static Dictionary<StorageKeys, string> StagingStorageDicts = new Dictionary<StorageKeys, string> {
            { StorageKeys.accesscontrol,"DefaultEndpointsProtocol=https;AccountName=3xaccesscontrolstaging;AccountKey=F+6krQZu/Ti1iBH3CS7Bn86C26TQ7uFtRY2WsI+Rb4I6CpuT3RVv9mxAYbrQ2EpaNp59SGhsIiWLTkMdiZqtXQ==;EndpointSuffix=core.windows.net"},
            { StorageKeys.alarms,"DefaultEndpointsProtocol=https;AccountName=3xalarmsstaging;AccountKey=mQLntnlO3LEfO5Hmh7yMs75Ifb1H6RsGeQjuikq7eNf2+udTRQMieac6cydBDtGUFq9zKLwgAzOQRxZm+AjJ/w==;EndpointSuffix=core.windows.net"},
            { StorageKeys.analytics,"DefaultEndpointsProtocol=https;AccountName=3xanalyticsstaging;AccountKey=f3Q3r6SxBaf/qgJ+iJtv/e/CDkHT0FFOGFb/Elzeu8oTtFknNr8mB2Z07PklTUtKL26sjCkTAfiKJvg6M5ZaVQ==;EndpointSuffix=core.windows.net"},
            { StorageKeys.appservice,"DefaultEndpointsProtocol=https;AccountName=3xappservicestaging;AccountKey=9MMZvS17Gl2EPq2DyqkvQLe7f+XGT/6BmWSXoX4rrkYNoiprdlWut9vU2V85JvZ4q8md4/epA425IoooelO0zw==;EndpointSuffix=core.windows.net"},
            { StorageKeys.audit,"DefaultEndpointsProtocol=https;AccountName=3xauditstaging;AccountKey=eKnszuqJaUmLRKS1PLuPlEmaMlLPTA6lA60WyWTXSo5vX7qCPgoD5H+D7XFkXniCgFVrozg9Dt+GpXcUGn1rFA==;EndpointSuffix=core.windows.net"},
            { StorageKeys.cache,"DefaultEndpointsProtocol=https;AccountName=3xcachestaging;AccountKey=TG15TPzngIP/vUQN85aVfOMBU6Qew7gey78h1YB7zmZztWRNTEp0ie6x38ENrphPUuCQP6xwGup2VF+/CZfKsw==;EndpointSuffix=core.windows.net"},
            { StorageKeys.centralmonitoring,"DefaultEndpointsProtocol=https;AccountName=3xcentralmonitorstaging;AccountKey=knGNbLdyPJygVuDjjmoclvaELH9CqRIwZqw/ZqX2cXqFggXuAvJG+/bT2WaXd6XVToIF7AvjYRKJ2AxpuSUTYg==;EndpointSuffix=core.windows.net"},
            { StorageKeys.email,"DefaultEndpointsProtocol=https;AccountName=3xemailstaging;AccountKey=qOYBgN0ssnf2zSqBNgDK0rOtK/pVmp7jMLsbYuFze/Pnu59oECKWv4sbTVGhNX0Zr14XxXwgPCJYAbBkamqI3g==;EndpointSuffix=core.windows.net"},
            { StorageKeys.posdata,"DefaultEndpointsProtocol=https;AccountName=3xposdatastaging;AccountKey=EpI5K3oljtFDLnCtRo+CWTDF4LpKt1/YhXibSN5VHLKUbMnXB4738HXtMcFGSe58mQovYXjtTA8z0DoRR/g4MQ==;EndpointSuffix=core.windows.net"},
            { StorageKeys.queue,"DefaultEndpointsProtocol=https;AccountName=3xqueuestaging;AccountKey=9kaKTzWhbCW2ZVHq/QYpfE/1Dg2jU+GupmHwm0Yaptwk4u3RCzvDPpdyr+00Et5AcTt4kSxcbSt0Y0iJv+UTKg==;EndpointSuffix=core.windows.net"},
            { StorageKeys.store,"DefaultEndpointsProtocol=https;AccountName=3xstorestaging;AccountKey=X4VdcS85QOC2TQcmpsahRQpayJwMUITPAfceHOPmeZHkhjNcIR21Mk1zLvbNNi2G/glRqzq9iCaUV46ShOjSpg==;EndpointSuffix=core.windows.net"},
            { StorageKeys.trends,"DefaultEndpointsProtocol=https;AccountName=3xtrendsstaging;AccountKey=7irExbcnDjq4Tm22IiUmzAd2GkJtClJIes9ofL2+TIOOpF9K3IFo85ZyqP7CXGkGgtkblq/xR8XqKWPwx0Jgdg==;EndpointSuffix=core.windows.net"},
            { StorageKeys.upload,"DefaultEndpointsProtocol=https;AccountName=3xuploadstaging;AccountKey=+6kHfnqs2SYZGLEHeaJjMi1orSrD/NpFvMwxNhpWmd9L7d5VBoPzW5vVGFjmzoTR+ZaFrcFmgPFrVPqD560FAQ==;EndpointSuffix=core.windows.net"},
            { StorageKeys.videojobs,"DefaultEndpointsProtocol=https;AccountName=3xvideojobsstaging;AccountKey=F18cKQUK//fsn9xU6/FHBTg/MRy0sx9hgPOYXIb2xMmR3FaK4x6/yXQg17XqzpP7airuwXcTXQ5vavwkJQOhJQ==;EndpointSuffix=core.windows.net"},
            { StorageKeys.flashfoodstransfer,"DefaultEndpointsProtocol=https;AccountName=flashfoodstftaging;AccountKey=4+PKR/FlQ1PxvB63P7ILreIgLdoTWJVckZv0CYNQ2q524o6En0mWav//SmzVug24MNKwJ4cNsuOgGMjcEQn6bQ==;EndpointSuffix=core.windows.net"},
            { StorageKeys.pandatransfer,"DefaultEndpointsProtocol=https;AccountName=pandatransferstaging;AccountKey=RNoB8pYr1MxK/PtZXF5VwxrEC/vgGfxyE8PqwHnkLVlsupWKvHTPUV9KHy3sg2Hf9tLB1yNNJ2r/cXK7JVOM5Q==;EndpointSuffix=core.windows.net"},
            { StorageKeys.biglotstransfer,"DefaultEndpointsProtocol=https;AccountName=biglotstransferstaging;AccountKey=r4PMeL6xE6E9NXdqAMz0eJ8cOg6/EsvgKDClv60GsmikNQ7nOg9U6OH4Cczv5c0dO2LHQhFPkIPNEzHdYdbLbg==;EndpointSuffix=core.windows.net"},
            { StorageKeys.rubiostransfer,"DefaultEndpointsProtocol=https;AccountName=rubiostransferstaging;AccountKey=VH2bkNLjwaTyhpjYomRTwXMfAap/haqP3YKE29bG/e5bR+m2wHsZz/+Ey4V71k4C8HTjFR4xvi39sFXFQEAIpg==;EndpointSuffix=core.windows.net"},
            { StorageKeys.circlekposdata,"DefaultEndpointsProtocol=https;AccountName=circlekposdata;AccountKey=4pyDpSfkp/U7P5w3q5EAFjU4T1a8FLGb0WFLWSsLt65Je7Ixy+2bjxsKaHbAYd5CGPznnF4+TCVsz4spDLrOvQ==;EndpointSuffix=core.windows.net"},
            { StorageKeys.vdcstorage,"DefaultEndpointsProtocol=https;AccountName=vdcdatastorage;AccountKey=WjDcTOPd3StY913IJ8RBT5zGU0kVnm0J8es2J5iRApPwUOSqn5fB52V1kmbT2JsGM89YPwN9bu/5Fa1E3EBlOA==;EndpointSuffix=core.windows.net"},
            { StorageKeys.migrationservice,"DefaultEndpointsProtocol=https;AccountName=3xmigrationservicedev;AccountKey=btHApQOuBhk4+5g1FTqTagu4RrO1E6+EhI0GQlXgD5RxishZviORhkDaw07ukq8zhFQo+A1JRSwgCnY/qBnRqw==;EndpointSuffix=core.windows.net"}

        };

        private static Dictionary<DatabaseKeys, string> ProdDBDicts = new Dictionary<DatabaseKeys, string>
        {
            { DatabaseKeys.managementservice,"Server=tcp:xdtsaf00dx.database.windows.net,1433;Database=ManagementService;User ID=TrendsLogin;Password=!1QAZ@2WSX#3EDC;Trusted_Connection=False;Encrypt=True;Connection Timeout=60;" },
            { DatabaseKeys.vdcdatabase,"Data Source=tcp:nticfl14e8.database.windows.net,1433;Initial Catalog=VDC_Root;User ID=TrendsLogin;Password=!1QAZ@2WSX#3EDC;MultipleActiveResultSets=True;Connection Timeout=60;" },
            { DatabaseKeys.vigilip,"Server=tcp:nb7ktdh11k.database.windows.net,1433;Initial Catalog=VIGIL-IP;Persist Security Info=False;User ID=TrendsLogin;Password=!1QAZ@2WSX#3EDC;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=60;" },
            { DatabaseKeys.posdm,"Data Source=vigilpos.cloudapp.net,2025;Initial Catalog=posdm;Persist Security Info=True;User ID=usr_bigdata;Password=g875rv68bfxxA"}
        };

        private static Dictionary<DatabaseKeys, string> DevDBDicts = new Dictionary<DatabaseKeys, string>
        {
            { DatabaseKeys.managementservice,"Server=tcp:xdtsaf00dx.database.windows.net,1433;Database=ManagementService_sso4;User ID=TrendsLogin;Password=!1QAZ@2WSX#3EDC;Trusted_Connection=False;Encrypt=True;Connection Timeout=60;" },
            { DatabaseKeys.vdcdatabase,"Data Source=tcp:nticfl14e8.database.windows.net,1433;Initial Catalog=VDC_Root;User ID=TrendsLogin;Password=!1QAZ@2WSX#3EDC;MultipleActiveResultSets=True;Connection Timeout=60;" },
            { DatabaseKeys.vigilip,"Server=tcp:nb7ktdh11k.database.windows.net,1433;Initial Catalog=VIGIL-IP;Persist Security Info=False;User ID=TrendsLogin;Password=!1QAZ@2WSX#3EDC;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=60;" },
            { DatabaseKeys.posdm,"Data Source=vigilpos.cloudapp.net,2025;Initial Catalog=posdm;Persist Security Info=True;User ID=usr_bigdata;Password=g875rv68bfxxA"}
        };

        private static Dictionary<DatabaseKeys, string> StagingDBDicts = new Dictionary<DatabaseKeys, string>
        {
            { DatabaseKeys.managementservice,"Server=tcp:xdtsaf00dx.database.windows.net,1433;Database=ManagementService_Staging;User ID=TrendsLogin;Password=!1QAZ@2WSX#3EDC;Trusted_Connection=False;Encrypt=True;Connection Timeout=60;" },
            { DatabaseKeys.vdcdatabase,"Data Source=tcp:nticfl14e8.database.windows.net,1433;Initial Catalog=VDC_Root;User ID=TrendsLogin;Password=!1QAZ@2WSX#3EDC;MultipleActiveResultSets=True;Connection Timeout=60;" },
            { DatabaseKeys.vigilip,"Server=tcp:nb7ktdh11k.database.windows.net,1433;Initial Catalog=VIGIL-IP;Persist Security Info=False;User ID=TrendsLogin;Password=!1QAZ@2WSX#3EDC;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=60;" },
            { DatabaseKeys.posdm,"Data Source=vigilpos.cloudapp.net,2025;Initial Catalog=posdm;Persist Security Info=True;User ID=usr_bigdata;Password=g875rv68bfxxA"}
        };

        private static Dictionary<DocumentDBKeys, string> ProdDocDBDicts = new Dictionary<DocumentDBKeys, string>
        {
            { DocumentDBKeys.classificationsmonitoring,"AccountEndpoint=https://classificationsmonitoring.documents.azure.com:443/;AccountKey=pOf4uVRtKSYAoVU17gmzL8uHVoeDSgGIUTc9jvaXhwyjItxWDdDaC25aOX6qyqv6NWcy4VuBldjrG0SD3dTOIQ==;" }
        };

        private static Dictionary<DocumentDBKeys, string> DevDocDBDicts = new Dictionary<DocumentDBKeys, string>
        {
            { DocumentDBKeys.classificationsmonitoring,"AccountEndpoint=https://classificationsmonitoringdev.documents.azure.com:443/;AccountKey=lkdE0D0V88qvGoFMPyoqSYea3XZHcol3dtNRsw2kTJhB8amJMorwgyyY3NFKCFjdmEIZP7RJbucJls0YutavhA==;" }
        };

        private static Dictionary<DocumentDBKeys, string> StagingDocDBDicts = new Dictionary<DocumentDBKeys, string>
        {
            { DocumentDBKeys.classificationsmonitoring,"AccountEndpoint=https://classificationsmonitoringdev.documents.azure.com:443/;AccountKey=lkdE0D0V88qvGoFMPyoqSYea3XZHcol3dtNRsw2kTJhB8amJMorwgyyY3NFKCFjdmEIZP7RJbucJls0YutavhA==;" }
        };
    }

    enum TrendsEnvironment
    {
        Prod = 0,
        Dev = 1,
        Staging = 2
    }

    enum StorageKeys
    {
        accesscontrol,
        alarms,
        analytics,
        appservice,
        audit,
        cache,
        centralmonitoring,
        email,
        posdata,
        queue,
        store,
        trends,
        upload,
        videojobs,
        flashfoodstransfer,
        pandatransfer,
        biglotstransfer,
        rubiostransfer,
        circlekposdata,
        vdcstorage,
        migrationservice
    }

    enum DatabaseKeys
    {
        managementservice,
        vdcdatabase,
        vigilip,
        posdm
    }

    enum DocumentDBKeys
    {
        classificationsmonitoring
    }
}
