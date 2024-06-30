<template>
    <div class="row" v-if="isValid('CanViewSignUpUser')">        

        <div class="col-lg-12">
            <div class="row">
                <div class="col-lg-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('SignUp.SignUpDetails') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('SignUp.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('SignUp.SignUpDetails') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddSignUpUser')" v-on:click="AddSignup" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('Categories.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                                    {{ $t('Categories.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-header">
                    <div class="input-group">
                        <button class="btn btn-secondary" type="button" id="button-addon1"><i class="fas fa-search"></i></button>
                        <input v-model="searchQuery" type="text" class="form-control" :placeholder="$t('SignUp.SearchBYNameAndEmail')" aria-label="Example text with button addon" aria-describedby="button-addon1">
                    </div>
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>#</th>

                                    <th>
                                        {{ $t('SignUp.USERNAME') }}
                                    </th>
                                    <th>
                                        {{ $t('SignUp.EMAILID') }}
                                    </th>
                                    <th>
                                        {{ $t('SignUp.UserRole') }}
                                    </th>
                                    <th>
                                        {{ $t('SignUp.Location') }}
                                    </th>

                                    <th class="text-right">
                                        {{ $t('SignUp.Status') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(details,index) in resultQuery" v-bind:key="details.id">
                                    <td>
                                        {{index+1}}
                                    </td>
                                    <td v-if="isValid('CanEditSignUpUser')">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditInfo(details.id,false)">{{details.fullName}}</a>
                                        </strong>
                                    </td>
                                    <td v-else>
                                        {{details.fullName}}
                                    </td>
                                    <td>{{details.email}}</td>
                                    <td>{{details.roleName}}</td>
                                    <td>{{details.companyName}}</td>

                                    <td v-if="details.isActive" class="text-right d-flex justify-content-right">
                                        <button class="btn btn-sm      ml-1 mr-1 " v-on:click="EditInfo(details.id,true)" title="Permission">
                                            <i class="fa fa-lock"></i>
                                        </button>
                                        <div class="form-check form-switch pt-1" v-if="details.employeeNo!='EM-00001'">
                                            <input class="form-check-input" v-on:change="EditEmployeeStatus(details.id, true)"    type="checkbox">
                                        </div>
                                        <a href="javascript:void(0)" class="btn btn-sm  ml-1 mr-1  pt-1 " v-on:click="GetEmployeeCode(details)" v-if="details.employeeId==null"><i class="fa fa-user"></i></a>

                                    </td>
                                    <td v-else class="text-right d-flex justify-content-right">
                                        <button class="btn btn-sm   ml-1 mr-1 " v-on:click="EditInfo(details.id,true)" title="Permission">
                                            <i class="fa fa-lock"></i>
                                        </button>
                                        <div class="form-check form-switch pt-1" v-if="details.employeeNo!='EM-00001'">
                                            <input class="form-check-input" v-on:change="EditEmployeeStatus(details.id, false)" checked   type="checkbox">
                                        </div>
                                        <a href="javascript:void(0)" class="btn btn-sm  ml-1 mr-1  pt-1 " v-on:click="GetEmployeeCode(details)" v-if="details.employeeId==null"><i class="fa fa-user"></i></a>

                                    </td>

                                </tr>
                            </tbody>
                        </table>
                    </div>

                </div>
            </div>


        </div>

    </div>
    <div v-else> <acessdenied></acessdenied></div>
</template>
<script>
    import moment from 'moment';
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        name: 'signUp',
        data: function () {
            return {
                searchQuery: '',
                show: false,
                loading: false,
                loginList:[

                ],
                user: {
                    id: '',
                    isActive: '',
                    isUser: true,
                },

            }
        },
        computed: {
            resultQuery: function () {
                var root = this;
                if (this.searchQuery) {
                    return this.loginList.filter((city) => {
                        return root.searchQuery.toLowerCase().split(' ').every(v => city.email.toLowerCase().includes(v) || city.fullName.toLowerCase().includes(v))
                    })
                } else {
                    return root.loginList;
                }
            },
        },

        methods: {
            AddSignup: function () {
               this.$router.push('/AddSignUp') 
            },
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            GetData: function () {
                var root = this;
                var url = '/account/UserList';
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.loginList = response.data;
                    }
                });
            },
            GetEmployeeCode: function (user) {
                
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https
                    .get('/EmployeeRegistration/EmployeeCode', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data != null) {

                            var employee = {
                                id: '00000000-0000-0000-0000-000000000000',
                                code: response.data,
                                registrationDate: moment().format('llll'),
                                englishName: user.fullName,
                                gender: '',
                                idNumber: '',
                                arabicName: '',
                                martialStatus: '',
                                employeeType: '',
                                nationality: '',
                                dateOfBirth: '',
                                mobileNo: '',
                                isSignup: true,
                                email: user.email,

                            };
                            root.$router.push({
                                path: '/addEmployeeRegistration',
                                query: {
                                    Add: true,
                                    data: employee
                                }
                            })

                        }
                    });
            },

            EditInfo: function (id, isPermission) {
                
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/account/UserDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        if (isPermission) {
                            if (root.loginList != "" && root.loginList != null && root.loginList != undefined) {
                                var getRole = root.loginList.find(x => x.id == id).roleName;
                            }       
                            root.$store.GetEditObj(response.data);                   
                            root.$router.push({
                                path: '/AddLoginPermission',
                                query: { data: '',
                                        Add: false, rolename: getRole }
                            })
                           
                        }
                        else {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/AddSignUp',
                                query: { 
                                        data: '',
                                        Add: false, 
                                        }
                            })
                        }
                        
                    }
                });
                
            },
            DeleteData: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/account/UserDelete?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        
                        root.$swal.fire({
                            icon:'warning',
                            title:'Deleted Successfully',
                            showConfirmButton: false,
                            timer: 800,
                            timerProgressBar: true,
                        });
                        root.GetData();
                    }
                });
            },

            EditEmployeeStatus: function (Id, isActive) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                this.user.id = Id;
                this.user.isActive = !isActive;

                root.$https.post('/EmployeeRegistration/SaveEmployeeStatus', root.user, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != '00000000-0000-0000-0000-000000000000') {
                            root.GetData();
                            root.$swal.fire({
                                icon: 'success',
                                title: 'Employee Status Change',
                                showConfirmButton: false,
                                timer: 1800,
                                timerProgressBar: true,

                            });
                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        });

            }

        },
        created: function () {
            this.$emit('update:modelValue', this.$route.name);
        },
        mounted: function () {
            this.GetData();
        }
    }
</script>