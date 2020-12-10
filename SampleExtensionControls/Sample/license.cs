using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample
{
    public static class License
    {
        public const string Key =
            "ABQBFAIUB21TAGEAbQBwAGwAZQDlX4MH4XTMZxjZ1VF1DSq7w9dK05phM+A50HLn" +
            "9tPxqH2PnYEtbZeE+cheJCa4jTwCvAp44z61NP6EWxd7+WRDz68t+q3pzLEG/Tdl" +
            "xt6yk7fqCLEbmgL+WKc/Otud9MLehagb/YajMBL0JhXQDLSAtkepl2chmBBaDmnb" +
            "ebmMwnGb6OpHEIPCHYDfTycRRyWHaoZHH2cKNHu5HukLair9xFhlI7Ab+UIOv52w" +
            "HuSP5Bq8apKlzikhglDsIaiQwvr3LZRWm9QKZt8deuL9QFNUaDBzf/M2GdhpDm39" +
            "PNVcIriroFZQV8rmKca2EMhYOe+hkqQEPIYoJbnH1ZBDaV64am5xLWYOA3lWZlZq" +
            "eAKNWoxI8AplzKa1RrPPvZRsg3eDRzKz4IVegDiy04rc3EeLfr+GD/Bzr5SsiIqS" +
            "zh1Xct6/9sRWbLpmMMijt4q2rBjzxzk5u4JLipdoL2Rdk3s5UdWqhltOah1r+vjS" +
            "igv/gVm5bzaL0GJ7WLnaKfxVB2QUrZiOoAuaxr8KvwTBxtzbM88ccyQCdWGl6obQ" +
            "LVrvB08b78FoHe3IbRsEJ3t3fpC7E7glFdvcjdNF1lpTOI1Yn1mAPTh/QD9FN0Zy" +
            "HyexsKx8VNdHUg7yzeAuq67OifrwvLrx5itZmsI7gp8Ey2rkfHNIywJnWhEojamS" +
            "ibuwlDCCBVUwggQ9oAMCAQICEEEDeNImNll6Ftsmxr0QlIswDQYJKoZIhvcNAQEF" +
            "BQAwgbQxCzAJBgNVBAYTAlVTMRcwFQYDVQQKEw5WZXJpU2lnbiwgSW5jLjEfMB0G" +
            "A1UECxMWVmVyaVNpZ24gVHJ1c3QgTmV0d29yazE7MDkGA1UECxMyVGVybXMgb2Yg" +
            "dXNlIGF0IGh0dHBzOi8vd3d3LnZlcmlzaWduLmNvbS9ycGEgKGMpMTAxLjAsBgNV" +
            "BAMTJVZlcmlTaWduIENsYXNzIDMgQ29kZSBTaWduaW5nIDIwMTAgQ0EwHhcNMTQx" +
            "MjExMDAwMDAwWhcNMTUxMjIyMjM1OTU5WjCBhjELMAkGA1UEBhMCSlAxDzANBgNV" +
            "BAgTBk1peWFnaTEYMBYGA1UEBxMPU2VuZGFpIEl6dW1pLWt1MRcwFQYDVQQKFA5H" +
            "cmFwZUNpdHkgaW5jLjEaMBgGA1UECxQRVG9vbHMgRGV2ZWxvcG1lbnQxFzAVBgNV" +
            "BAMUDkdyYXBlQ2l0eSBpbmMuMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKC" +
            "AQEAwQL2ymVbspWkCpEHpVUHtcmbz5rrTHvwdlY2a8COz96uanuluHwz0di4RVNG" +
            "PwfhtpfEViriLvl7mQ2vuz6cZsnlR8zoKV2pt5GxDjO9Fvqel+u1w4HB9g7HTCh5" +
            "hB8jpXMtXOE9saNQMrqp0dkt/8Ry9Igq9Fu7cgs4TeS67HTuBCRv76utIFTIkpdT" +
            "ydbxz4r72x9aodg9vwUXYhrNbGGZ8h0igM0rKOvev/AifeNB6Omp9qaIc2xT87bo" +
            "pLQRy8JLkIU4oNPq+92cCR6TeTItZ5/5xr9xsWjvi9rBga2bDbDPD+FzCUA0hBoI" +
            "DHP7kkdBndISDwstJn4LwThP7wIDAQABo4IBjTCCAYkwCQYDVR0TBAIwADAOBgNV" +
            "HQ8BAf8EBAMCB4AwKwYDVR0fBCQwIjAgoB6gHIYaaHR0cDovL3NmLnN5bWNiLmNv" +
            "bS9zZi5jcmwwZgYDVR0gBF8wXTBbBgtghkgBhvhFAQcXAzBMMCMGCCsGAQUFBwIB" +
            "FhdodHRwczovL2Quc3ltY2IuY29tL2NwczAlBggrBgEFBQcCAjAZDBdodHRwczov" +
            "L2Quc3ltY2IuY29tL3JwYTATBgNVHSUEDDAKBggrBgEFBQcDAzBXBggrBgEFBQcB" +
            "AQRLMEkwHwYIKwYBBQUHMAGGE2h0dHA6Ly9zZi5zeW1jZC5jb20wJgYIKwYBBQUH" +
            "MAKGGmh0dHA6Ly9zZi5zeW1jYi5jb20vc2YuY3J0MB8GA1UdIwQYMBaAFM+Zqep7" +
            "JvRLyY6P1/AFJu/j0qedMB0GA1UdDgQWBBQAWvCtpdR4NfWEEqgsBQ74VhuOjjAR" +
            "BglghkgBhvhCAQEEBAMCBBAwFgYKKwYBBAGCNwIBGwQIMAYBAQABAf8wDQYJKoZI" +
            "hvcNAQEFBQADggEBAIjCmFo3jlvlWIqxF8IDqFtV6oyE0ImYvriarF1i/DeCwXig" +
            "4IOiIzqRaHLU2hR3Yulyv0+N8YnnllfixmWqjF5+VOkeCdfww8m4qkMGyTtaSGIS" +
            "7rl8HBv6D3BAcwx+BjSCMcgBDZkR/Y8npNNIVy+PbjCHvd2zKpyaPb3R+nAO0doX" +
            "aMTmmr+1AE4ny8OQ3jrC3ioyEbqcik6Bz0qeDIst0Q7tXfgozU1v6w30mSpNZc2g" +
            "2qU5/tCNgfCXDsq7tbeQgYr5/WQ/XGpMGlfCwETmwuWe6M/4kCpXxoqUEkMpEjci" +
            "GWsb0IlSaoU2GZnZ/lATmMC89B5d68ucxiKomuAwggK6MIIBoqADAgECAgh3Yvy+" +
            "uOrZ0DANBgkqhkiG9w0BAQUFADAcMRowGAYDVQQDDBFHQy1TVTExNTAwLTY2NjY2" +
            "NjAgFw0xNTAxMDEwMDAwMDBaGA8yMDU1MDQzMDAwMDAwMFowHDEaMBgGA1UEAwwR" +
            "R0MtU1UxMTUwMC02NjY2NjYwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIB" +
            "AQDtjjewiO/8hUNpyOXr4AtAj40HVwSUvxecUfY5CFyY5zMTLdKPn8vlYb0bV+mY" +
            "7JL40M5uPv/c7piHiN1ZmWE1+ZjPlkLl1d3NEhTswg63wdPnUXDiy1+4Cfmhj4SC" +
            "p64BdelupJgP1/YmAaUA8qdJ6BoOZVklawbqzgW4gATlG+oFZYHJNupNuYiaDvez" +
            "yyRGKG+wscfZ4og/uzhmlINdQjgJ5+1nj5FMZrMmfzx6zUYzPx8k+hcu1gU+vh+Q" +
            "wKYQ4w+OY4hv6iTIbZ6scAlXT3Eydqd+fnGj9t8gkNMPsUvDtkOBeegDrj3TEsbq" +
            "xUDesauEiPMmTCH7FSN7fDCfAgMBAAEwDQYJKoZIhvcNAQEFBQADggEBAG+rH4k2" +
            "wy0X0QIOJbjpg/WRQM8umG53Xda6TF74NUAC55MAu0MaaVuUxoKBM4s34iVtx2gR" +
            "p0iE3tQJaOVrKkBcKO872iOarYlsEvvZMCqj27sHsYJK8QFUoZXXRCgTekAqTb6u" +
            "TFWGNYYEoxxv63sAXJZSM3MnGrfhi3TMU6ORUP6mGe0cxpPDB1jpMlSbZaL0TU2j" +
            "JTGlolAZuGc72rqv0VvtJdE4pi/2IJ4qPH1SD0pnOplBMG981wfynn9TOmkhntZG" +
            "J/odsi/sYbvZxlRwcvhymWjA+nRJUXosajT/hvDXWzdqbw5CFcgTMH1NkqyjizQa" +
            "oZ5dDxelzpqX+DY=";
    }


}
