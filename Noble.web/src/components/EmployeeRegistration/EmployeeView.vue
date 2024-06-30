<template>

    <div class="row" v-if="isValid('CanAddEmployeeReg') || isValid('CanEditEmployeeReg') ">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-lg-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title" >Employee Detail</h4>
                              
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-body">
                    <div class="dastone-profile">
                                        <div class="row">
                                            <div class="col-lg-4 align-self-center mb-3 mb-lg-0">
                                                <div class="dastone-profile-main">
                                                    <div class="dastone-profile-main-pic">
                                                        <img src="assets/images/users/user-4.jpg" alt="" height="110" class="rounded-circle">
                                                        <span class="dastone-profile_main-pic-change">
                                                            <i class="fas fa-camera"></i>
                                                        </span>
                                                    </div>
                                                    <div class="dastone-profile_user-detail">
                                                        <h5 class="dastone-user-name">{{ employee.englishName }} {{ employee.fatherName }}</h5>                                                        
                                                        <p class="mb-0 dastone-user-name-post">{{ employee.desigantionName }}</p>                                                        
                                                    </div>
                                                </div>                                                
                                            </div><!--end col-->
                                            
                                            <div class="col-lg-4 ms-auto align-self-center">
                                                <ul class="list-unstyled personal-detail mb-0">
                                                    <li class=""> <b>Code </b> : {{employee.code}}</li>
                                                    <li class="mt-2"> <b> Registration Date </b> : {{convertDate(employee.registrationDate)}}</li>
                                                    <li class=""> <b> Employee Type </b> : {{employee.employeeType}}</li>                                                 
                                                </ul>
                                               
                                            </div><!--end col-->
                                            <div class="col-lg-4 ms-auto align-self-center">
                                                <ul class="list-unstyled personal-detail mb-0">
                                                    <li class=""> <b>Department </b> : {{employee.departmentName}}</li>
                                                    <li class="mt-2"> <b> Designation</b> : {{employee.desigantionName}}</li>
                                                    <li class=""> <b> ID Number </b> : {{employee.idNumber}}</li>                                                 
                                                </ul>
                                               
                                            </div><!--end col-->
                                            <!-- <div class="col-lg-4 ms-auto align-self-center">
                                                <ul class="list-unstyled personal-detail mb-0">
                                                    <li class=""><i class="ti ti-mobile me-2 text-secondary font-16 align-middle"></i> <b> Phone </b> : {{employee.mobileNo}}</li>
                                                    <li class="mt-2"><i class="ti ti-email text-secondary font-16 align-middle me-2"></i> <b> Email </b> : {{employee.email}}</li>
                                                    <li class=""><i class="ti ti-mobile me-2 text-secondary font-16 align-middle"></i> <b> Other Contact Info </b> : {{employee.otherContact}}</li>                                                 
                                                </ul>
                                               
                                            </div> -->
                                        </div><!--end row-->
                                        <div class="row">
                                            <div class="alert alert-primary border-0" role="alert">
                                            <strong>Personal info :</strong> 
                                        </div>
                                            
                                            <div class="col-lg-4 align-self-center mb-3 mb-lg-0">
                                                <ul class="list-unstyled personal-detail mb-0">
                                                    <li > <b> Email </b> : {{employee.email}}</li>
                                                    <li class="mt-2"> <b> Date of Birth</b> : {{convertDate(employee.dateOfBirth)}}</li>
                                                    <li class=""> <b> Martial Status </b> : {{employee.martialStatus}}</li>                                                 

                                                </ul>                                        
                                            </div><!--end col-->
                                            
                                            <div class="col-lg-4 ms-auto align-self-center">
                                                <ul class="list-unstyled personal-detail mb-0">
                                                    <li class=""> <b> Phone </b> : {{employee.mobileNo}}</li>
                                                    <li class="mt-2"> <b>Gender </b> : {{employee.gender}}</li>
                                                    <li > <b> Avalibility </b> : {{employee.employeeAccess}}</li>
                                                </ul>
                                               
                                            </div><!--end col-->
                                            <div class="col-lg-4 ms-auto align-self-center">
                                                <ul class="list-unstyled personal-detail mb-0">
                                                    <li class=""> <b> Other Contact : </b>  {{employee.otherContact}}</li>    
                                                    <li class="mt-2"> <b> Blood Group </b> : {{employee.bloodGroup}}</li>                                                 
                                             
                                                    <li class=""> <b>Nationality </b> : {{employee.nationality}}</li>
                                                </ul>
                                               
                                            </div><!--end col-->
                                           
                                        </div><!--end row-->
                                        <div class="row">
                                            <div class="alert alert-primary border-0" role="alert">
                                            <strong>Residance :</strong> 
                                        </div>
                                            
                                            <div class="col-lg-4 ms-auto align-self-center">
                                                <ul class="list-unstyled personal-detail mb-0">
                                                    <li class=""> <b>National/Foreign </b> : {{employee.nationalOrForeign}}</li>
                                                </ul>
                                               
                                            </div><!--end col-->
                                            <div class="col-lg-4 ms-auto align-self-center">
                                                <ul class="list-unstyled personal-detail mb-0">
                                                    <li > <b> Country </b> : {{employee.country}}</li>
                                                </ul>
                                               
                                            </div><!--end col-->
                                            <div class="col-lg-4 ms-auto align-self-center">
                                                <ul class="list-unstyled personal-detail mb-0">
                                                    <li class=""> <b> City </b> : {{employee.city}}</li>   
                                                </ul>
                                               
                                            </div><!--end col-->
                                            <div class="col-lg-12 ms-auto align-self-center">
                                                <ul class="list-unstyled personal-detail mb-0">
                                                    <li class=""> <b>Address </b> : {{employee.address}}</li>
                                                                                                    
                                                </ul>
                                               
                                            </div><!--end col-->
                                          
                                           
                                        </div><!--end row-->
                                        <div class="row">
                                            <div class="alert alert-primary border-0" role="alert">
                                            <strong>Bank Info :</strong> 
                                        </div>
                                            
                                           <div class="col-lg-4 ms-auto align-self-center">
                                               <ul class="list-unstyled personal-detail mb-0">
                                                   <li class=""> <b>Bank Name </b> : {{employee.bankName}}</li>
                                                   <li class="mt-2"> <b> Account Name</b> : {{employee.accountName}}</li>
                                               </ul>
                                              
                                           </div><!--end col-->
                                           <div class="col-lg-4 ms-auto align-self-center">
                                               <ul class="list-unstyled personal-detail mb-0">
                                                   <li class=""> <b>Branch Name</b> : {{employee.branchName}}</li>
                                                   <li class="mt-2"> <b>Account Number </b> : {{employee.accountNumber}}</li>
                                               </ul>
                                              
                                           </div><!--end col-->
                                           <div class="col-lg-4 ms-auto align-self-center">
                                                <ul class="list-unstyled personal-detail mb-0">
                                                    <li class=""> <b>IBAN Number</b> : {{employee.ibanNumber}}</li>
                                                   <li class="mt-2"> <b>Account Type </b> : {{employee.accountType}}</li>
                                                                                                    
                                                </ul>
                                               
                                            </div><!--end col-->
                                            <div class="col-lg-12 ms-auto align-self-center">
                                                <ul class="list-unstyled personal-detail mb-0">
                                                    <li class=""> <b>Bank Address</b> : {{employee.bankAddress}}</li>
                                                                                                    
                                                </ul>
                                               
                                            </div><!--end col-->
                                          
                                         
                                          
                                       </div><!--end row-->
                                    </div><!--end f_profile-->


                  

                </div>
               
               
            </div>
        </div>
        <bulk-attachment :attachmentList="employee.attachmentList" :show="show" v-if="show" @close="attachmentSave" />
         <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>
    <div v-else> <acessdenied></acessdenied></div>

</template>
<script>
    import { required, maxLength, email, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'

    import moment from 'moment';
    
    import clickMixin from '@/Mixins/clickMixin'
     import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default ({
        mixins: [clickMixin],
        setup() {
            return { v$: useVuelidate() }
        },
         components: {
            Loading
        },
        data: function () {
            return {
                IsPaksitanClient: false,
                arabic: '',
                english: '',
                value: '',
                loading: false,
                active: 'PersonalInfo',
                language: 'Nothing',
                daterander: 0,
                salaryTypeOptions: [],
                employee: {
                    id: '00000000-0000-0000-0000-000000000000',
                    code: '',
                    registrationDate: '',
                    englishName: '',
                    fatherName: '',
                    salaryType: '',
                    arabicName: '',
                    gender: '',
                    martialStatus: '',
                    employeeType: '',
                    nationality: '',
                    dateOfBirth: '',
                    mobileNo: '',
                    otherContact: '',
                    email: '',
                    bloodGroup: '',
                    city: '',
                    isSignup: false,
                    isActive: true,
                    country: '',
                    address: '',
                    primaryNameOfPerson: '',
                    primaryRelation: '',
                    primaryContactNumber: '',
                    primaryReferenceEmail: '',
                    secondaryNameOfPerson: '',
                    secondaryRelation: '',
                    secondaryContactNumber: '',
                    secondaryReferenceEmail: '',
                    homePersonName: '',
                    homeRelation: '',
                    homeContactNumber: '',
                    homeReferenceEmail: '',
                    homeCity: '',
                    homeCountry: '',
                    idNumber: '',
                    idType: '',
                    title: '',
                    expiryDate: '',
                    passportNumber: '',
                    passportIssueDate: '',
                    passportExpiryDate: '',
                    passportIssuingAuthority: '',
                    drivingLicenseNumber: '',
                    drivingLicenseType: '',
                    drivingExpiryDate: '',
                    drivingIssuingAuthority: '',
                    medicalPolicNumber: '',
                    medicalPolicType: '',
                    medicalPolicProvider: '',
                    medicalPolicExpiryDate: '',
                    cnic: '',
                    photo: '',
                    drivingLicense: '',
                    passport: '',
                    designationId: '',
                    departmentId: '',
                    reason: '',
                    bankName: '',
                    AccountName: '',
                    IbanNumber: '',
                    accountNumber: '',
                    branchName: '',
                    bankAddress: '',
                    accountType: '',
                    employeeAccess: '',
                    perDayWorkHour: 0,
                    employeeAttachments: [{
                        cnic: '',
                        photo: '',
                        drivingLicense: '',
                        passport: ''
                    }],
                    nationalOrForeign: '',
                    spouseName1: '',
                    spouseName2: '',
                    spouseName3: '',
                    spouseName4: '',
                    days: 0,
                    limit: 0,
                    temporaryCashReceiver: false,
                    temporaryCashIssuer: false,
                    temporaryCashRequester: false,
                    attachmentList: [],
                },
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                search: '',
                isAddAttachment: false,
                emailExist: false,
                show: false,
                cityRender: 0,
            }
        },
        validations() {
          return{
              employee:
            {
                code: { required },
                registrationDate: { required },
                englishName: {
                    maxLength: maxLength(30)
                },
                arabicName: {
                    required: requiredIf(function () {
                            return !this.employee.englishName;
                        }),
                    // required: requiredIf((x) => {
                    //     if (x.englishName == '' || x.englishName == null)
                    //         return true;
                    //     return false;
                    // }),
                    maxLength: maxLength(40)
                },
                gender: { required },
                martialStatus: {},
                employeeType: {},
                dateOfBirth: {},
                mobileNo: { maxLength: maxLength(25) },
                email: { required, email },
                address: { maxLength: maxLength(350) },
                idNumber: { required },

                bloodGroup: {},
                otherContact: {},
                city: {},
                country: {},
                nationality: {},
                primaryNameOfPerson: {},
                primaryRelation: {},
                primaryContactNumber: {},
                primaryReferenceEmail: {},
                secondaryNameOfPerson: {},
                secondaryRelation: {},
                secondaryContactNumber: {},
                secondaryReferenceEmail: {},
                homePersonName: {},
                homeRelation: {},
                homeContactNumber: {},
                homeReferenceEmail: {},
                homeCity: {},
                homeCountry: {},
                idType: {},
                title: {},
                expiryDate: {},
                passportNumber: {},
                passportIssueDate: {},
                passportExpiryDate: {},
                passportIssuingAuthority: {},
                drivingLicenseNumber: {},
                drivingLicenseType: {},
                drivingExpiryDate: {},
                drivingIssuingAuthority: {},
                medicalPolicNumber: {},
                medicalPolicType: {},
                medicalPolicProvider: {},
                medicalPolicExpiryDate: {}
            }
          }
        },
        methods: {
            convertDate: function (x) {
                return moment(x).format('DD MMM YYYY');
            },

            Attachment: function () {
                this.show = true;
            },

            attachmentSave: function (attachment) {
                this.employee.attachmentList = attachment;
                this.show = false;
            },

            SetCity: function () {
                this.cityRender++;
            },
            ChangeCode: function () {
            
            var value;
            if (this.employee.employeeType == 'Permanent') {
                value = 'Pr';
            }
            else if (this.employee.employeeType == 'Probation') {
                value = 'Pb';
            }
            else if (this.employee.employeeType == 'Internee') {
                value = 'In';
            }
            else if (this.employee.employeeType == 'Temporary') {
                value = 'Tm';
            }
            let str = this.employee.code;
            if (str.indexOf("-") !== -1 && str.indexOf("-", str.indexOf("-") + 1) !== -1) 
            {
               let originalString = str;
                let regex = /-(.*?)-/; // matches any text between underscores
                let replacementText = value;

                let modifiedString = originalString.replace(regex, "-" + replacementText  + "-");
                this.employee.code=modifiedString;
            }
           
            else {
                let splitIndex = this.employee.code.indexOf("-") + 1; // Find the index of "-" and add 1 to include it in the second part
                this.employee.code = this.employee.code.slice(0, splitIndex) + value + '-' + this.employee.code.slice(splitIndex);
            }


        },

            languageChange: function (lan) {
                if (this.language == lan) {
                    if (this.employee.id == '00000000-0000-0000-0000-000000000000') {

                        var getLocale = this.$i18n.locale;
                        this.language = getLocale;

                        this.$router.go('/addEmployeeRegistration');
                    }
                    else {

                        this.$swal({
                            title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'You cannot Change language during update, otherwise your current page data will be lose!' : 'لا يمكنك تغيير اللغة أثناء التحديث ، وإلا ستفقد بيانات صفحتك الحالية!',
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            icon: 'error',
                            timer: 4000,
                            timerProgressBar: true,
                        });
                    }
                }


            },
            makeActive: function (tab) {

                this.active = tab;
            },
            EmailDuplicate: function (x) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/account/DuplicateEmail?email=' + x, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data == true) {
                            root.emailExist = true;
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Email Already Exist!' : 'البريد الالكتروني موجود مسبقا!',
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1700,
                                timerProgressBar: true,
                            });
                        }
                        else {
                            root.emailExist = false;
                        }


                    })
            },

            uploadImage(type) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                var file = null;

                if (type == 'cnic') {
                    file = this.$refs.imgupload1.files;
                }

                if (type == 'drivingLicense') {
                    file = this.$refs.imgupload2.files;
                }

                if (type == 'passport') {
                    file = this.$refs.imgupload3.files;
                }

                if (type == 'photo') {
                    file = this.$refs.imgupload4.files;
                }

                var fileData = new FormData();
                for (var k = 0; k < file.length; k++) {
                    fileData.append("files", file[k]);
                }
                root.$https.post('/company/UploadFilesAsync', fileData, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            if (type == 'cnic') {
                                root.employee.cnic = response.data;
                            }

                            if (type == 'drivingLicense') {
                                root.employee.drivingLicense = response.data;
                            }

                            if (type == 'passport') {
                                root.employee.passport = response.data;
                            }

                            if (type == 'photo') {
                                root.employee.photo = response.data;
                            }
                        }
                    });
            },

            DownloadAttachment(path) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var ext = path.split('.')[1];
                root.$https.get('/contact/DownloadFile?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` }, responseType: 'blob' })
                    .then(function (response) {
                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;
                        link.setAttribute('download', 'file.' + ext);
                        document.body.appendChild(link);
                        link.click();
                    });
            },
            GetAutoCodeGenerator: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https
                    .get('/EmployeeRegistration/EmployeeCode', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data != null) {
                            root.employee.code = response.data;
                        }
                    });
            },
            Cancel: function () {
                if (this.isValid('CanViewEmployeeReg')) {
                    this.$router.push({
                        path: '/employeeRegistration',

                    })
                }
                else {
                    this.$router.go();
                }
            },


            SaveEmployee: function () {
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (this.emailExist) {
                    this.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                        text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Email Already Exist!' : 'البريد الالكتروني موجود مسبقا!',
                        type: 'error',
                        icon: 'error',
                        showConfirmButton: false,
                        timer: 1700,
                        timerProgressBar: true,
                    });
                }
                else {
                    if (this.employee.employeeType == '' || this.employee.employeeType == undefined) {
                        this.employee.employeeType = 'default';

                    }
                    root.$https
                        .post('/EmployeeRegistration/SaveEmployee', this.employee, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(response => {
                            root.loading = false
                            root.info = response.data.bpi

                            root.$swal.fire({
                                icon: 'success',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully' : 'حفظ بنجاح',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved' : 'تم الحفظ',

                                showConfirmButton: false,
                                timer: 1800,
                                timerProgressBar: true,

                            });
                            if (root.isValid('CanViewEmployeeReg')) {
                                root.$router.push({
                                    path: '/employeeRegistration',

                                })
                            }
                            else {
                                root.$router.go();
                            }
                        })
                        .catch(error => {
                            console.log(error)
                            root.$swal.fire(
                                {
                                    icon: 'error',
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Server Error!' : 'خطأ في الخادم',
                                    text: error.response.data,
                                    showConfirmButton: false,
                                    timer: 5000,
                                    timerProgressBar: true,
                                });

                            root.loading = false
                        })
                        .finally(() => root.loading = false)
                }
            },

            GetEmployeeAttachments: function (search, currentPage, id) {
                var root = this;
                var url = '/EmployeeRegistration/GetEmployeeAttachments?searchTerm=' + search + '&pageNumber=' + currentPage + '&id=' + id;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.results.length > 0) {

                        root.employee.employeeAttachments = response.data.results;
                        root.employee.cnic = response.data.results[0].cnic;
                        root.employee.photo = response.data.results[0].photo;
                        root.employee.drivingLicense = response.data.results[0].drivingLicense;
                        root.employee.passport = response.data.results[0].passport;
                    }
                    else {
                        root.employee.employeeAttachments = [{
                            cnic: '',
                            photo: '',
                            drivingLicense: '',
                            passport: ''
                        }]

                    }

                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;

                });
            }
        },
        created: function () {
            var root =  this;

            if(root.$route.query.Add == 'false')
            {
                root.$route.query.data = this.$store.isGetEdit;
            }
            this.$emit('update:modelValue', this.$route.name);
            if (this.$route.query.data != undefined) {
                this.employee = this.$route.query.data;
                this.GetEmployeeAttachments(this.search, 1, this.employee.id);

                this.daterander++;
            }
            else {
                this.employee.salaryType = (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Bank' : 'بنكي';
            }
        },
        filters: {
            formatDate: function (value) {
                return moment(value).format("DD MMM yyyy hh:mm");
            }
        },
        mounted: function () {
            this.language = this.$i18n.locale;
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.IsPaksitanClient = localStorage.getItem('IsPaksitanClient') == "true" ? true : false;

            if (this.$route.query.data == undefined || this.$route.query.data == '') {
                //eslint-dsiable-line
                this.GetAutoCodeGenerator();
                this.employee.registrationDate = moment().format('llll');
            }

            if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                this.salaryTypeOptions = ['Cash', 'Bank'];
            }
            else {
                this.salaryTypeOptions = ['الـنـقـدي', 'بنكي'];
            }
        }
    })

</script>