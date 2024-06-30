export default {

    methods: {
        isProduction: function (x) {
            var isProduction = JSON.parse(localStorage.getItem("IsProduction"));
            if (x === isProduction) {
                return true;
            }
        },
        checkArabicNumberMixinFunction: function (modifiedValue) {
            
            if(/^[0-9\u0660-\u0669]+$/.test(modifiedValue)==true)
                {
                    modifiedValue = modifiedValue.replace(/[٠-٩]/g, d => "٠١٢٣٤٥٦٧٨٩".indexOf(d)).replace(/[۰-۹]/g, d => "۰۱۲۳۴۵۶۷۸۹".indexOf(d));
                 return modifiedValue;
                }
                else
                {
                    return modifiedValue;
                }
        },
        isMonthPicker: function () {
            if (this.$i18n.locale == 'en') 
                return 'en'
            
            else if (this.$i18n.locale == 'ar')
                return 'ar'
            else if (this.$i18n.locale == 'pt')
                return 'pt'
            else
                return 'en'

        },
        isValid: function (x) {
           
            var storedColors = JSON.parse(localStorage.getItem("permission"));
            if (x === 'Noble Admin') {
                return true;
            }
            else if (storedColors != undefined) {


                var isFound = storedColors.find(function (item) {
                    if (item.permissionCategory === x) {
                        return true;

                    }
                    return false;
                });
                if (isFound !== undefined) {
                    return true;
                }
                else {
                    return false;
                }
            }

        },
        
        isLeftToRight: function () {
           
            return localStorage.getItem('LeftToRight') == "true" ? true : false;


        },
       
        isOtherLang: function () {
            var getArabicLanguage = localStorage.getItem('Arabic');
            var getPortuguesLanguage = localStorage.getItem('Portugues');
            if (getArabicLanguage == 'true' || getPortuguesLanguage == 'true')
                return true
            return false



        },
        languageForCSV: function () {
            var english = localStorage.getItem('English') == 'true' ? true : false;
            var arabic = localStorage.getItem('Arabic') == 'true' ? true : false;
            if (english && arabic) {
                return 'ar';
            }
            if (arabic) {
                return 'ar';
            }
            else {
                return 'en'
            }
        },
        
        //GenerateToken: function (y) {
        //    

        //    var permissions = localStorage.getItem('permission');
        //    var permissionList = JSON.parse(permissions);
        //    var claims = {};
        //    var roles = [];
        //    roles.push(localStorage.getItem('RoleName') );
        //    permissionList.forEach(function (x) {
        //        if (x.moduleName === y)
        //            roles.push(x.permissionCategory );
        //    });
        //    //console.log(roles);
        //    var header = {
        //        "alg": "HS256",
        //        "typ": "JWT"
        //    };

        //    //Decode General Token
        //    var jwt = localStorage.getItem('token');

        //    var tokens = jwt.split(".");

        //    var decode = atob(tokens[1]).split(",");
        //    //var splitByKeyValue =  decode[14].split(':')[0] + decode[14].split(':')[1] ;
        //   // decode[14] = splitByKeyValue + ':' + roles;
        //   // decode[0] = decode[0].substring(1);
        //   //decode = decode.join();
        //    decode[0] = decode[0].substring(1);
        //    decode[decode.length - 1] = decode[decode.length - 1].substring(0,-1);

        //    decode.forEach(function (x) {
        //        var y = x.replace(/"/g, '').split(':');
        //        //var heading = y[0].replace(/\"/g, '');
        //        var index = y.indexOf("http");
        //        if (index > -1) {
        //            if (index === 0) {
        //                claims[y[0] + ':' + y[1]] = y[2];
        //            } else {
        //                claims[y[0]] = y[1] + ':' + y[2];
        //            }
        //        } else {
        //            claims[y[0]] = y[1];
        //        }
               
        //    });
        //    claims["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] = roles
        //    claims["exp"] = 1658475128
            
        //    //Decoding End
        //    //var jwt1 = require('jsonwebtoken');
        //    // token1 = jwt1.sign(claims, 'shhhhh');
        //   // console.log(token1);
        //    var stringifiedHeader = this.$CryptoJS.enc.Utf8.parse(JSON.stringify(header));
        //    var encodedHeader = this.base64url(stringifiedHeader);

           

        //    var stringifiedData = this.$CryptoJS.enc.Utf8.parse(JSON.stringify(claims));
        //    var encodedData = this.base64url(stringifiedData);

        //    var token = encodedHeader + "." + encodedData;
        //    var secret = "SOME_RANDOM_KEY_DO_NOT_SHARE";



        //    var jwt1 = require('jsonwebtoken');
        //    var token1 = jwt1.sign(claims, 'SOME_RANDOM_KEY_DO_NOT_SHARE', { algorithm: 'HS256' });
        //    console.log(token1)

        //    var signature = this.$CryptoJS.HmacSHA256(token, this.$CryptoJS.enc.Utf8.parse(JSON.stringify(secret)));
        //    signature = this.base64url(signature);

        //    var signedToken = token + "." + signature;
        //    console.log(signedToken)
        //    localStorage.setItem('token', signedToken);
        //},

        //base64url: function (source) {
        //    var encodedSource = this.$CryptoJS.enc.Base64.stringify(source);

        //    // Remove padding equal characters
        //    encodedSource = encodedSource.replace(/=+$/, '');

        //    // Replace characters according to base64url specifications
        //    encodedSource = encodedSource.replace(/\+/g, '-');
        //    encodedSource = encodedSource.replace(/\//g, '_');

        //    return encodedSource;
        //}

      
    }
 
}

