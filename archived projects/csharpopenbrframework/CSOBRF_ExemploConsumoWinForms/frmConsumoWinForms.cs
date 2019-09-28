using CSOBRF_Criptografia;
using CSOBRF_Util.Grafico;
using CSOBRF_Validacoes;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace CSOBRF_ExemploConsumoWinForms
{
    public partial class frmConsumoWinForms : Form
    {
        //esse abaixo é o array de chaves randômicas (usado para ativação) - você pode trocar aqui e criar suas chaves personalizadas (não esqueça de trocar também na AtivacaoSoftware.cs)
        string[] arrayChaves = { "A4IYG2ZYU5", "A18IPG12ZO", "A27IPG21ZO", "A36IPG30ZO", "A45IPG39ZO", "A54IPG48ZO", "A63IPG57ZO", "A72IPG66ZO", "A81IPG75ZO", "A90IPG84ZO", "A99IPG93ZO", "A108IG102Z", "A117IG111Z", "A126IG120Z", "A135IG129Z", "A144IG138Z", "A153IG147Z", "A162IG156Z", "A171IG165Z", "A180IG174Z", "A189IG183Z", "A198IG192Z", "A207IG201Z", "A216IG210Z", "A225IG219Z", "A234IG228Z", "A243IG237Z", "A252IG246Z", "A261IG255Z", "A270IG264Z", "A279IG273Z", "A288IG282Z", "A297IG291Z", "A306IG300Z", "A315IG309Z", "A324IG318Z", "A333IG327Z", "A342IG336Z", "A351IG345Z", "A360IG354Z", "A369IG363Z", "A378IG372Z", "A387IG381Z", "A396IG390Z", "A405IG399Z", "A414IG408Z", "A423IG417Z", "A432IG426Z", "A441IG435Z", "A450IG444Z", "A459IG453Z", "A468IG462Z", "A477IG471Z", "A486IG480Z", "A495IG489Z", "A504IG498Z", "A513IG507Z", "A522IG516Z", "A531IG525Z", "A540IG534Z", "A549IG543Z", "A558IG552Z", "A567IG561Z", "A576IG570Z", "A585IG579Z", "A594IG588Z", "A603IG597Z", "A612IG606Z", "A621IG615Z", "A630IG624Z", "A639IG633Z", "A648IG642Z", "A657IG651Z", "A666IG660Z", "A675IG669Z", "A684IG678Z", "A693IG687Z", "A702IG696Z", "A711IG705Z", "A720IG714Z", "A729IG723Z", "A738IG732Z", "A747IG741Z", "A756IG750Z", "A765IG759Z", "A774IG768Z", "A783IG777Z", "A792IG786Z", "A801IG795Z", "A810IG804Z", "A819IG813Z", "A828IG822Z", "A837IG831Z", "A846IG840Z", "A855IG849Z", "A864IG858Z", "A873IG867Z", "A882IG876Z", "A891IG885Z", "A900IG894Z", "A900IG894Z", "A507IG508Z", "A512IG513Z", "A517IG518Z", "A522IG523Z", "A527IG528Z", "A532IG533Z", "A537IG538Z", "A542IG543Z", "A547IG548Z", "A552IG553Z", "A557IG558Z", "A562IG563Z", "A567IG568Z", "A572IG573Z", "A577IG578Z", "A582IG583Z", "A587IG588Z", "A592IG593Z", "A597IG598Z", "A602IG603Z", "A607IG608Z", "A612IG613Z", "A617IG618Z", "A622IG623Z", "A627IG628Z", "A632IG633Z", "A637IG638Z", "A642IG643Z", "A647IG648Z", "A652IG653Z", "A657IG658Z", "A662IG663Z", "A667IG668Z", "A672IG673Z", "A677IG678Z", "A682IG683Z", "A687IG688Z", "A692IG693Z", "A697IG698Z", "A702IG703Z", "A707IG708Z", "A712IG713Z", "A717IG718Z", "A722IG723Z", "A727IG728Z", "A732IG733Z", "A737IG738Z", "A742IG743Z", "A747IG748Z", "A752IG753Z", "A757IG758Z", "A762IG763Z", "A767IG768Z", "A772IG773Z", "A777IG778Z", "A782IG783Z", "A787IG788Z", "A792IG793Z", "A797IG798Z", "A802IG803Z", "A807IG808Z", "A812IG813Z", "A817IG818Z", "A822IG823Z", "A827IG828Z", "A832IG833Z", "A837IG838Z", "A842IG843Z", "A847IG848Z", "A852IG853Z", "A857IG858Z", "A862IG863Z", "A867IG868Z", "A872IG873Z", "A877IG878Z", "A882IG883Z", "A887IG888Z", "A892IG893Z", "A897IG898Z", "A902IG903Z", "A907IG908Z", "A912IG913Z", "A917IG918Z", "A922IG923Z", "A927IG928Z", "A932IG933Z", "A937IG938Z", "A942IG943Z", "A947IG948Z", "A952IG953Z", "A957IG958Z", "A962IG963Z", "A967IG968Z", "A972IG973Z", "A977IG978Z", "A982IG983Z", "A987IG988Z", "A992IG993Z", "A997IG998Z", "A997IG998Z", "A1410G1413", "A1417G1420", "A1424G1427", "A1431G1434", "A1438G1441", "A1445G1448", "A1452G1455", "A1459G1462", "A1466G1469", "A1473G1476", "A1480G1483", "A1487G1490", "A1494G1497", "A1501G1504", "A1508G1511", "A1515G1518", "A1522G1525", "A1529G1532", "A1536G1539", "A1543G1546", "A1550G1553", "A1557G1560", "A1564G1567", "A1571G1574", "A1578G1581", "A1585G1588", "A1592G1595", "A1599G1602", "A1606G1609", "A1613G1616", "A1620G1623", "A1627G1630", "A1634G1637", "A1641G1644", "A1648G1651", "A1655G1658", "A1662G1665", "A1669G1672", "A1676G1679", "A1683G1686", "A1690G1693", "A1697G1700", "A1704G1707", "A1711G1714", "A1718G1721", "A1725G1728", "A1732G1735", "A1739G1742", "A1746G1749", "A1753G1756", "A1760G1763", "A1767G1770", "A1774G1777", "A1781G1784", "A1788G1791", "A1795G1798", "A1802G1805", "A1809G1812", "A1816G1819", "A1823G1826", "A1830G1833", "A1837G1840", "A1844G1847", "A1851G1854", "A1858G1861", "A1865G1868", "A1872G1875", "A1879G1882", "A1886G1889", "A1893G1896", "A1900G1903", "A1907G1910", "A1914G1917", "A1921G1924", "A1928G1931", "A1935G1938", "A1942G1945", "A1949G1952", "A1956G1959", "A1963G1966", "A1970G1973", "A1977G1980", "A1984G1987", "A1991G1994", "A1998G2001", "A2005G2008", "A2012G2015", "A2019G2022", "A2026G2029", "A2033G2036", "A2040G2043", "A2047G2050", "A2054G2057", "A2061G2064", "A2068G2071", "A2075G2078", "A2082G2085", "A2089G2092", "A2096G2099", "A2096G2099", "A2710G2711", "A2719G2720", "A2728G2729", "A2737G2738", "A2746G2747", "A2755G2756", "A2764G2765", "A2773G2774", "A2782G2783", "A2791G2792", "A2800G2801", "A2809G2810", "A2818G2819", "A2827G2828", "A2836G2837", "A2845G2846", "A2854G2855", "A2863G2864", "A2872G2873", "A2881G2882", "A2890G2891", "A2899G2900", "A2908G2909", "A2917G2918", "A2926G2927", "A2935G2936", "A2944G2945", "A2953G2954", "A2962G2963", "A2971G2972", "A2980G2981", "A2989G2990", "A2998G2999", "A3007G3008", "A3016G3017", "A3025G3026", "A3034G3035", "A3043G3044", "A3052G3053", "A3061G3062", "A3070G3071", "A3079G3080", "A3088G3089", "A3097G3098", "A3106G3107", "A3115G3116", "A3124G3125", "A3133G3134", "A3142G3143", "A3151G3152", "A3160G3161", "A3169G3170", "A3178G3179", "A3187G3188", "A3196G3197", "A3205G3206", "A3214G3215", "A3223G3224", "A3232G3233", "A3241G3242", "A3250G3251", "A3259G3260", "A3268G3269", "A3277G3278", "A3286G3287", "A3295G3296", "A3304G3305", "A3313G3314", "A3322G3323", "A3331G3332", "A3340G3341", "A3349G3350", "A3358G3359", "A3367G3368", "A3376G3377", "A3385G3386", "A3394G3395", "A3403G3404", "A3412G3413", "A3421G3422", "A3430G3431", "A3439G3440", "A3448G3449", "A3457G3458", "A3466G3467", "A3475G3476", "A3484G3485", "A3493G3494", "A3502G3503", "A3511G3512", "A3520G3521", "A3529G3530", "A3538G3539", "A3547G3548", "A3556G3557", "A3565G3566", "A3574G3575", "A3583G3584", "A3592G3593", "A3592G3593", "A3615G3612", "A3624G3621", "A3633G3630", "A3642G3639", "A3651G3648", "A3660G3657", "A3669G3666", "A3678G3675", "A3687G3684", "A3696G3693", "A3705G3702", "A3714G3711", "A3723G3720", "A3732G3729", "A3741G3738", "A3750G3747", "A3759G3756", "A3768G3765", "A3777G3774", "A3786G3783", "A3795G3792", "A3804G3801", "A3813G3810", "A3822G3819", "A3831G3828", "A3840G3837", "A3849G3846", "A3858G3855", "A3867G3864", "A3876G3873", "A3885G3882", "A3894G3891", "A3903G3900", "A3912G3909", "A3921G3918", "A3930G3927", "A3939G3936", "A3948G3945", "A3957G3954", "A3966G3963", "A3975G3972", "A3984G3981", "A3993G3990", "A4002G3999", "A4011G4008", "A4020G4017", "A4029G4026", "A4038G4035", "A4047G4044", "A4056G4053", "A4065G4062", "A4074G4071", "A4083G4080", "A4092G4089", "A4101G4098", "A4110G4107", "A4119G4116", "A4128G4125", "A4137G4134", "A4146G4143", "A4155G4152", "A4164G4161", "A4173G4170", "A4182G4179", "A4191G4188", "A4200G4197", "A4209G4206", "A4218G4215", "A4227G4224", "A4236G4233", "A4245G4242", "A4254G4251", "A4263G4260", "A4272G4269", "A4281G4278", "A4290G4287", "A4299G4296", "A4308G4305", "A4317G4314", "A4326G4323", "A4335G4332", "A4344G4341", "A4353G4350", "A4362G4359", "A4371G4368", "A4380G4377", "A4389G4386", "A4398G4395", "A4407G4404", "A4416G4413", "A4425G4422", "A4434G4431", "A4443G4440", "A4452G4449", "A4461G4458", "A4470G4467", "A4479G4476", "A4488G4485", "A4497G4494", "A4497G4494", "A4013G4016", "A4021G4024", "A4029G4032", "A4037G4040", "A4045G4048", "A4053G4056", "A4061G4064", "A4069G4072", "A4077G4080", "A4085G4088", "A4093G4096", "A4101G4104", "A4109G4112", "A4117G4120", "A4125G4128", "A4133G4136", "A4141G4144", "A4149G4152", "A4157G4160", "A4165G4168", "A4173G4176", "A4181G4184", "A4189G4192", "A4197G4200", "A4205G4208", "A4213G4216", "A4221G4224", "A4229G4232", "A4237G4240", "A4245G4248", "A4253G4256", "A4261G4264", "A4269G4272", "A4277G4280", "A4285G4288", "A4293G4296", "A4301G4304", "A4309G4312", "A4317G4320", "A4325G4328", "A4333G4336", "A4341G4344", "A4349G4352", "A4357G4360", "A4365G4368", "A4373G4376", "A4381G4384", "A4389G4392", "A4397G4400", "A4405G4408", "A4413G4416", "A4421G4424", "A4429G4432", "A4437G4440", "A4445G4448", "A4453G4456", "A4461G4464", "A4469G4472", "A4477G4480", "A4485G4488", "A4493G4496", "A4501G4504", "A4509G4512", "A4517G4520", "A4525G4528", "A4533G4536", "A4541G4544", "A4549G4552", "A4557G4560", "A4565G4568", "A4573G4576", "A4581G4584", "A4589G4592", "A4597G4600", "A4605G4608", "A4613G4616", "A4621G4624", "A4629G4632", "A4637G4640", "A4645G4648", "A4653G4656", "A4661G4664", "A4669G4672", "A4677G4680", "A4685G4688", "A4693G4696", "A4701G4704", "A4709G4712", "A4717G4720", "A4725G4728", "A4733G4736", "A4741G4744", "A4749G4752", "A4757G4760", "A4765G4768", "A4773G4776", "A4781G4784", "A4789G4792", "A4797G4800", "A4797G4800", "A1206G1207", "A1208G1209", "A1210G1211", "A1212G1213", "A1214G1215", "A1216G1217", "A1218G1219", "A1220G1221", "A1222G1223", "A1224G1225", "A1226G1227", "A1228G1229", "A1230G1231", "A1232G1233", "A1234G1235", "A1236G1237", "A1238G1239", "A1240G1241", "A1242G1243", "A1244G1245", "A1246G1247", "A1248G1249", "A1250G1251", "A1252G1253", "A1254G1255", "A1256G1257", "A1258G1259", "A1260G1261", "A1262G1263", "A1264G1265", "A1266G1267", "A1268G1269", "A1270G1271", "A1272G1273", "A1274G1275", "A1276G1277", "A1278G1279", "A1280G1281", "A1282G1283", "A1284G1285", "A1286G1287", "A1288G1289", "A1290G1291", "A1292G1293", "A1294G1295", "A1296G1297", "A1298G1299", "A1300G1301", "A1302G1303", "A1304G1305", "A1306G1307", "A1308G1309", "A1310G1311", "A1312G1313", "A1314G1315", "A1316G1317", "A1318G1319", "A1320G1321", "A1322G1323", "A1324G1325", "A1326G1327", "A1328G1329", "A1330G1331", "A1332G1333", "A1334G1335", "A1336G1337", "A1338G1339", "A1340G1341", "A1342G1343", "A1344G1345", "A1346G1347", "A1348G1349", "A1350G1351", "A1352G1353", "A1354G1355", "A1356G1357", "A1358G1359", "A1360G1361", "A1362G1363", "A1364G1365", "A1366G1367", "A1368G1369", "A1370G1371", "A1372G1373", "A1374G1375", "A1376G1377", "A1378G1379", "A1380G1381", "A1382G1383", "A1384G1385", "A1386G1387", "A1388G1389", "A1390G1391", "A1392G1393", "A1394G1395", "A1396G1397", "A1398G1399", "A1400G1401", "A1402G1403", "A1402G1403", "A3506G3510", "A3511G3515", "A3516G3520", "A3521G3525", "A3526G3530", "A3531G3535", "A3536G3540", "A3541G3545", "A3546G3550", "A3551G3555", "A3556G3560", "A3561G3565", "A3566G3570", "A3571G3575", "A3576G3580", "A3581G3585", "A3586G3590", "A3591G3595", "A3596G3600", "A3601G3605", "A3606G3610", "A3611G3615", "A3616G3620", "A3621G3625", "A3626G3630", "A3631G3635", "A3636G3640", "A3641G3645", "A3646G3650", "A3651G3655", "A3656G3660", "A3661G3665", "A3666G3670", "A3671G3675", "A3676G3680", "A3681G3685", "A3686G3690", "A3691G3695", "A3696G3700", "A3701G3705", "A3706G3710", "A3711G3715", "A3716G3720", "A3721G3725", "A3726G3730", "A3731G3735", "A3736G3740", "A3741G3745", "A3746G3750", "A3751G3755", "A3756G3760", "A3761G3765", "A3766G3770", "A3771G3775", "A3776G3780", "A3781G3785", "A3786G3790", "A3791G3795", "A3796G3800", "A3801G3805", "A3806G3810", "A3811G3815", "A3816G3820", "A3821G3825", "A3826G3830", "A3831G3835", "A3836G3840", "A3841G3845", "A3846G3850", "A3851G3855", "A3856G3860", "A3861G3865", "A3866G3870", "A3871G3875", "A3876G3880", "A3881G3885", "A3886G3890", "A3891G3895", "A3896G3900", "A3901G3905", "A3906G3910", "A3911G3915", "A3916G3920", "A3921G3925", "A3926G3930", "A3931G3935", "A3936G3940", "A3941G3945", "A3946G3950", "A3951G3955", "A3956G3960", "A3961G3965", "A3966G3970", "A3971G3975", "A3976G3980", "A3981G3985", "A3986G3990", "A3991G3995", "A3996G4000", "A3996G4000", "A3204G3204", "A3208G3208", "A3212G3212", "A3216G3216", "A3220G3220", "A3224G3224", "A3228G3228", "A3232G3232", "A3236G3236", "A3240G3240", "A3244G3244", "A3248G3248", "A3252G3252", "A3256G3256", "A3260G3260", "A3264G3264", "A3268G3268", "A3272G3272", "A3276G3276", "A3280G3280", "A3284G3284", "A3288G3288", "A3292G3292", "A3296G3296", "A3300G3300", "A3304G3304", "A3308G3308", "A3312G3312", "A3316G3316", "A3320G3320", "A3324G3324", "A3328G3328", "A3332G3332", "A3336G3336", "A3340G3340", "A3344G3344", "A3348G3348", "A3352G3352", "A3356G3356", "A3360G3360", "A3364G3364", "A3368G3368", "A3372G3372", "A3376G3376", "A3380G3380", "A3384G3384", "A3388G3388", "A3392G3392", "A3396G3396", "A3400G3400", "A3404G3404", "A3408G3408", "A3412G3412", "A3416G3416", "A3420G3420", "A3424G3424", "A3428G3428", "A3432G3432", "A3436G3436", "A3440G3440", "A3444G3444", "A3448G3448", "A3452G3452", "A3456G3456", "A3460G3460", "A3464G3464", "A3468G3468", "A3472G3472", "A3476G3476", "A3480G3480", "A3484G3484", "A3488G3488", "A3492G3492", "A3496G3496", "A3500G3500", "A3504G3504", "A3508G3508", "A3512G3512", "A3516G3516", "A3520G3520", "A3524G3524", "A3528G3528", "A3532G3532", "A3536G3536", "A3540G3540", "A3544G3544", "A3548G3548", "A3552G3552", "A3556G3556", "A3560G3560", "A3564G3564", "A3568G3568", "A3572G3572", "A3576G3576", "A3580G3580", "A3584G3584", "A3588G3588", "A3592G3592", "A3596G3596", "A3596G3596", "A1807G1805", "A1809G1807", "A1811G1809", "A1813G1811", "A1815G1813", "A1817G1815", "A1819G1817", "A1821G1819", "A1823G1821", "A1825G1823", "A1827G1825", "A1829G1827", "A1831G1829", "A1833G1831", "A1835G1833", "A1837G1835", "A1839G1837", "A1841G1839", "A1843G1841", "A1845G1843", "A1847G1845", "A1849G1847", "A1851G1849", "A1853G1851", "A1855G1853", "A1857G1855", "A1859G1857", "A1861G1859", "A1863G1861", "A1865G1863", "A1867G1865", "A1869G1867", "A1871G1869", "A1873G1871", "A1875G1873", "A1877G1875", "A1879G1877", "A1881G1879", "A1883G1881", "A1885G1883", "A1887G1885", "A1889G1887", "A1891G1889", "A1893G1891", "A1895G1893", "A1897G1895", "A1899G1897", "A1901G1899", "A1903G1901", "A1905G1903", "A1907G1905", "A1909G1907", "A1911G1909", "A1913G1911", "A1915G1913", "A1917G1915", "A1919G1917", "A1921G1919", "A1923G1921", "A1925G1923", "A1927G1925", "A1929G1927", "A1931G1929", "A1933G1931", "A1935G1933", "A1937G1935", "A1939G1937", "A1941G1939", "A1943G1941", "A1945G1943", "A1947G1945", "A1949G1947", "A1951G1949", "A1953G1951", "A1955G1953", "A1957G1955", "A1959G1957", "A1961G1959", "A1963G1961", "A1965G1963", "A1967G1965", "A1969G1967", "A1971G1969", "A1973G1971", "A1975G1973", "A1977G1975", "A1979G1977", "A1981G1979", "A1983G1981", "A1985G1983", "A1987G1985", "A1989G1987", "A1991G1989", "A1993G1991", "A1995G1993", "A1997G1995", "A1999G1997", "A2001G1999" };
        ValidacaoFormatacaoDeCampos valida = new ValidacaoFormatacaoDeCampos();
        AtivacaoSoftware ativa = new AtivacaoSoftware();
        NewContasMatematicas contas = new NewContasMatematicas();

        string chave = ""; //essa é a chave que o cliente DEVE digitar pra dar software válido
        string ChaveHD = "";
        string subChave = ""; //esse é o código de 3 digitos que será solicitada ao cliente
        string subHD = ""; //esse é o código de 5 digitos que será solicitada ao cliente

        #region Import da API Kernel do Windows e das Variaveis que receberão Informações do HardDisk da Máquina (S/N)
        //importa a API Kernel32 do windows
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        extern static bool GetVolumeInformation(
        string RootPathName,
        StringBuilder VolumeNameBuffer,
        int VolumeNameSize,
        out uint VolumeSerialNumber,
        out uint MaximumComponentLength,
        out uint FileSystemFlags,
        StringBuilder FileSystemNameBuffer,
        int nFileSystemNameSize);
        #endregion

        #region Construtor do Form
        public frmConsumoWinForms()
        {
            InitializeComponent();

            //gera código de ativação (leia a documentação na textbox na segunda aba)
            DataTable dt_Chaves = new DataTable();            
            dt_Chaves = ativa.voltarCodigoeChaveAtivacao("");
            
            subChave = dt_Chaves.Rows[0]["SubChave"].ToString(); 
            subHD = dt_Chaves.Rows[0]["SubHD"].ToString(); 
            //essas duas abaixo são as chaves que o cliente deve digitar pra dar Software Válido
            chave = dt_Chaves.Rows[0]["Chave"].ToString(); //primeiras duas casas de 5+5 que o cliente deve digitar
            ChaveHD = dt_Chaves.Rows[0]["ChaveHD"].ToString(); //terceira casa de 5 caracters que o cliente deve digitar
            tbxCodigoAtivacao.Text = subChave;
            tbxCodigoHd.Text = subHD;
        }
        #endregion

        #region Criptografia
        private void btnCripExecutar_Click(object sender, EventArgs e)
        {
            Criptografia crip = new Criptografia();
            if(rdbCripCriptografia.Checked)
            {
                tbxCripSaida.Text = crip.Criptografar(tbxCripValor.Text);
            }
            else
            {
                tbxCripSaida.Text = crip.Descriptografar(tbxCripValor.Text);
            }
        }

        #endregion

        #region Validação de Dados
        private void tbxFormatacaoTelefone_Leave(object sender, EventArgs e)
        {
            tbxFormatacaoTelefone.Text = valida.validaFormataTelefone(tbxFormatacaoTelefone.Text);
        }

        private void tbxLimpaString_Leave(object sender, EventArgs e)
        {
            tbxLimpaString.Text = valida.retiraPontuacao(tbxLimpaString.Text);
        }

        private void tbxValidaCpfCnpj_Leave(object sender, EventArgs e)
        {
            if (valida.ValidaCPF(tbxValidaCpfCnpj.Text) || valida.ValidaCNPJ(tbxValidaCpfCnpj.Text)) //verifica se o valor é um CPF ou CNPJ
            {
                tbxValidaCpfCnpj.Text = valida.pontuaCpf_CNPJ(tbxValidaCpfCnpj.Text);
            }
            else
            {
                tbxValidaCpfCnpj.Text = "Inválido";
            }
        }

        private void tbxAjusteDecimal_Leave(object sender, EventArgs e)
        {
            if (contas.verificaSeEInteiro(tbxAjusteDecimal.Text) || contas.verificaSeEDecimal(tbxAjusteDecimal.Text))
            {
                if (rdb2Casas.Checked)
                {
                    tbxAjusteDecimal.Text = contas.newValidaAjustaArredonda2CasasDecimais(tbxAjusteDecimal.Text);
                }
                if (rdb3Casas.Checked)
                {
                    tbxAjusteDecimal.Text = contas.newValidaAjustaArredonda3CasasDecimais(tbxAjusteDecimal.Text);
                }
                if (rdb4Casas.Checked)
                {
                    tbxAjusteDecimal.Text = contas.newValidaAjustaArredonda4CasasDecimais(tbxAjusteDecimal.Text);
                }
            }
        }

        private void tbxVerificaSeEDecimal_Leave(object sender, EventArgs e)
        {
            if (contas.verificaSeEDecimal(tbxVerificaSeEDecimal.Text))
            {
                MessageBox.Show("É decimal!");
            }
            else
            {
                MessageBox.Show("Não é decimal!");
            }
        }
        #endregion

        #region Pesquisa CEP
        private void tbxCEP_Leave(object sender, EventArgs e)
        {
            if(valida.ValidaCep(tbxCEP.Text))
            {
                string[] dadosCep = valida.retornaCepPeloWSCorreios(tbxCEP.Text);
                tbxEndereco.Text = dadosCep[0];
                tbxComplemento.Text = dadosCep[1] + " " + dadosCep[2];
                tbxCidade.Text = dadosCep[3];
                tbxBairro.Text = dadosCep[4];
                tbxUF.Text = dadosCep[5];
            }
        }
        #endregion

        #region Tratamento de Imagens
        private void btnAlterarImagem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileProcurarImagem = new OpenFileDialog();
            openFileProcurarImagem.Title = "Selecione uma imagem para o seu logo";
            openFileProcurarImagem.InitialDirectory = @"C:\";
            openFileProcurarImagem.RestoreDirectory = true;
            openFileProcurarImagem.Filter = "Imagens JPG (apenas formato JPG) (*.jpg)|*.jpg;";            

            if(!Directory.Exists(Directory.GetCurrentDirectory() + "\\IMAGENS\\"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\IMAGENS\\");
            }

            if (openFileProcurarImagem.ShowDialog() == DialogResult.OK)
            {
                Imagem img = new Imagem();
                                
                string retorno = new Imagem().gerarNovaImagem(Convert.ToInt32(tbxLargura.Text), Convert.ToInt32(tbxAltura.Text), openFileProcurarImagem.FileName.ToString(), 1, Convert.ToInt32(tbxCompressao.Text), true);

                tbxCaminhoImagem.Text = openFileProcurarImagem.FileName.ToString();
                Image imgObj = Image.FromFile(openFileProcurarImagem.FileName.ToString());
                pctImagemOrigem.Image = imgObj;
                pctImagemOrigem.Refresh();
                long tamanhoArquivoEntrada = new System.IO.FileInfo(openFileProcurarImagem.FileName.ToString()).Length;                
                tbxEntrada.Text = tamanhoArquivoEntrada.ToString() + " MB";

                if (retorno != "ERRO")//se ele conseguir gerar ele pega o novo caminho da nova imagem
                {
                    Image imgObjRet = Image.FromFile(retorno);
                    pctImagemSaida.Image = imgObjRet;
                    pctImagemSaida.Refresh();
                    tbxCaminhoSaida.Text = retorno;
                    long tamanhoArquivoSaida = new System.IO.FileInfo(retorno).Length;                    
                    tbxSaida.Text = tamanhoArquivoSaida.ToString() + " MB";
                }
                else//senão pega a normal mesmo sem compactacao
                {
                    retorno = openFileProcurarImagem.FileName.ToString();
                    tbxCaminhoSaida.Text = retorno;
                }
            }
        }

        private void btnAbrirImagemSaida_Click(object sender, EventArgs e)
        {
            if (tbxCaminhoSaida.Text != "")
            {
                Process.Start(tbxCaminhoSaida.Text);
            }
        }
        #endregion

        #region Métodos Referentes a Ativação de Software
        private void btnLiberarUsoSoftware_Click(object sender, EventArgs e)
        {
            if (tbxChaveCliente1.Text != "" && tbxChaveCliente2.Text != "" && tbxChaveCliente3.Text != "")
            {
                string chaveDigitada = tbxChaveCliente1.Text.ToString() + tbxChaveCliente2.Text.ToString();
                string chaveHDDigitada = tbxChaveCliente3.Text.ToString();
                if ((chaveDigitada == chave) && (ChaveHD == chaveHDDigitada))
                {
                    MessageBox.Show(null, "Chave ativada com sucesso! Agora você deve armazenar no seu software as informações, e liberar o seu sistema para uso", "Exemplo CSharpOpenBrFramework", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(null, "Chave Inválida!", "Exemplo CSharpOpenBrFramework", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Você deve gerar a chave de RETORNO (embaixo) inserindo o código de ativação do cliente. Então pegue o código de liberação e insira nas 3 caixas.");
            }
        }        

        private void btnGerarChaveParaOCliente_Click(object sender, EventArgs e)
        {
            if (tbxCodigoClienteAtivacao.Text != "" && tbxCodigoClienteHD.Text != "")
            {
                tbxCodigoRetorno1.Text = arrayChaves[Convert.ToInt32(tbxCodigoClienteAtivacao.Text)].ToString().Substring(0, 5);
                tbxCodigoRetorno2.Text = arrayChaves[Convert.ToInt32(tbxCodigoClienteAtivacao.Text)].ToString().Substring(5, 5);
                retornarQuintaChave();
            }
            else
            {
                MessageBox.Show("Insira o código 1 lá em cima. Essa é a chave que será pedida para o cliente.");
            }
        }

        /// <summary>
        /// Retorna a Quinta Chave de validação
        /// </summary>
        public void retornarQuintaChave()
        {
            string serialHD = retornaNumeroSerieHD();
            string quatroPrimeirosDigitosHD = tbxCodigoClienteHD.Text.ToString();

            //pega separadamente os 4 primeiros digitos do HD que serão usados na chave
            int priDigito = Convert.ToInt32(tbxCodigoClienteHD.Text.Substring(0, 1).ToString());
            int segDigito = Convert.ToInt32(tbxCodigoClienteHD.Text.Substring(1, 1).ToString());
            int terDigito = Convert.ToInt32(tbxCodigoClienteHD.Text.Substring(2, 1).ToString());
            int quaDigito = Convert.ToInt32(tbxCodigoClienteHD.Text.Substring(3, 1).ToString());
            int quiDigito = Convert.ToInt32(tbxCodigoClienteHD.Text.Substring(4, 1).ToString());

            string priLetra = "";
            string segLetra = "";
            string terLetra = "";
            string quaLetra = "";
            string quiLetra = "";

            if (priDigito == 0)
            {
                priLetra = "A";
            }

            if (priDigito == 1)
            {
                priLetra = "A";
            }

            if (priDigito == 2)
            {
                priLetra = "C";
            }

            if (priDigito == 3)
            {
                priLetra = "F";
            }

            if (priDigito == 4)
            {
                priLetra = "H";
            }

            if (priDigito == 5)
            {
                priLetra = "Y";
            }

            if (priDigito == 6)
            {
                priLetra = "Z";
            }

            if (priDigito == 7)
            {
                priLetra = "A";
            }

            if (priDigito == 8)
            {
                priLetra = "U";
            }

            if (priDigito == 9)
            {
                priLetra = "T";
            }

            if (segDigito == 0)
            {
                segLetra = "D";
            }

            if (segDigito == 1)
            {
                segLetra = "D";
            }

            if (segDigito == 2)
            {
                segLetra = "R";
            }

            if (segDigito == 3)
            {
                segLetra = "Y";
            }

            if (segDigito == 4)
            {
                segLetra = "U";
            }

            if (segDigito == 5)
            {
                segLetra = "K";
            }

            if (segDigito == 6)
            {
                segLetra = "W";
            }

            if (segDigito == 7)
            {
                segLetra = "O";
            }

            if (segDigito == 8)
            {
                segLetra = "N";
            }

            if (segDigito == 9)
            {
                segLetra = "V";
            }

            if (terDigito == 0)
            {
                terLetra = "J";
            }

            if (terDigito == 1)
            {
                terLetra = "J";
            }

            if (terDigito == 2)
            {
                terLetra = "P";
            }

            if (terDigito == 3)
            {
                terLetra = "Y";
            }

            if (terDigito == 4)
            {
                terLetra = "X";
            }

            if (terDigito == 5)
            {
                terLetra = "E";
            }

            if (terDigito == 6)
            {
                terLetra = "M";
            }

            if (terDigito == 7)
            {
                terLetra = "T";
            }

            if (terDigito == 8)
            {
                terLetra = "T";
            }

            if (terDigito == 9)
            {
                terLetra = "C";
            }

            if (quaDigito == 0)
            {
                quaLetra = "V";
            }

            if (quaDigito == 1)
            {
                quaLetra = "V";
            }

            if (quaDigito == 2)
            {
                quaLetra = "G";
            }

            if (quaDigito == 3)
            {
                quaLetra = "F";
            }

            if (quaDigito == 4)
            {
                quaLetra = "F";
            }

            if (quaDigito == 5)
            {
                quaLetra = "L";
            }

            if (quaDigito == 6)
            {
                quaLetra = "I";
            }

            if (quaDigito == 7)
            {
                quaLetra = "O";
            }

            if (quaDigito == 8)
            {
                quaLetra = "S";
            }

            if (quaDigito == 9)
            {
                quaLetra = "A";
            }

            if (quiDigito == 0)
            {
                quiLetra = "B";
            }

            if (quiDigito == 1)
            {
                quiLetra = "C";
            }

            if (quiDigito == 2)
            {
                quiLetra = "D";
            }

            if (quiDigito == 3)
            {
                quiLetra = "E";
            }

            if (quiDigito == 4)
            {
                quiLetra = "F";
            }

            if (quiDigito == 5)
            {
                quiLetra = "G";
            }

            if (quiDigito == 6)
            {
                quiLetra = "H";
            }

            if (quiDigito == 7)
            {
                quiLetra = "I";
            }

            if (quiDigito == 8)
            {
                quiLetra = "J";
            }

            if (quiDigito == 9)
            {
                quiLetra = "L";
            }

            string chaveFinal = priLetra + segLetra + terLetra + quaLetra + quiLetra;

            tbxCodigoRetorno3.Text = chaveFinal;
        }

        /// <summary>
        /// Verifica se o nome do HOST e o numero do HD do mesmo estão licenciados para usar o sistema
        /// </summary>
        /// <returns>Retorna numero Série HD</returns>
        public string retornaNumeroSerieHD()
        {
            StringBuilder volname = new StringBuilder(256);
            StringBuilder fsname = new StringBuilder(256);
            uint sernum, maxlen, flags;
            if (!GetVolumeInformation("c:\\", volname, volname.Capacity, out sernum, out maxlen, out flags, fsname, fsname.Capacity))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            string volnamestr = volname.ToString();
            string fsnamestr = fsname.ToString();

            string numeroSerieHDMaquina = Convert.ToString(sernum);
            return numeroSerieHDMaquina;
        }
        #endregion
    }
}
