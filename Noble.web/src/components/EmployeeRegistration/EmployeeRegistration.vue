<template>
    <div class="row" v-if="isValid('CanViewEmployeeReg')">

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('EmployeeRegistration.EmployeeRegistration') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('EmployeeRegistration.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('EmployeeRegistration.EmployeeRegistration') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('LeaveManagement') " v-on:click="EmployeeRegistration('',false)"
                                   href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    Employee Bulk Emails
                                </a>
                                <a v-if="isValid('CanAddEmployeeReg')" v-on:click="AddEmployee"
                                   href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('EmployeeRegistration.AddNew') }}
                                </a>
                                <a v-if="isValid('CanAddEmployeeReg')" v-on:click="openmodel"
                                   href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('EmployeeRegistration.QuickRegister') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('EmployeeRegistration.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-8" style="padding-top:20px">
                        <div class="input-group">
                            <button class="btn btn-soft-primary" type="button" id="button-addon1">
                                <i class="fas fa-search"></i>
                            </button>
                            <input v-model="search" type="text" class="form-control" :placeholder="$t('EmployeeRegistration.SearchbyBrand')"
                               aria-label="Example text with button addon" aria-describedby="button-addon1, button-addon2">
                            <a class="btn btn-outline-primary" v-on:click="FilterWareHouse" id="button-addon2">
                                <i class="fa fa-filter"></i>
                            </a>
                        </div>
                    </div>
                    <div class=" col-lg-4 mt-1" v-if="!advanceFilters">

<button v-on:click="search22(true)" :disabled ="!search" type="button" class="btn btn-outline-primary mt-3">
    {{ $t('Sale.ApplyFilter') }}
</button>
<button v-on:click="clearData(false)" :disabled ="!search" type="button" class="btn btn-outline-primary mx-2 mt-3">
    {{ $t('Sale.ClearFilter') }}
</button>

</div>
                    </div>
                    <br id="hide" v-if="advanceFilters" />

                    <div class="row" v-if="advanceFilters">
                            <div class="col-md-3 col-lg-3  col-12 form-group" id="hide" v-if="advanceFilters">
                                <label>Department :</label>
                                <departmentDropdown v-model="department"  @update:modelValue="GetFilter('department')" />
                            </div>
                            
                            <div class="col-md-3 col-lg-3  col-12 form-group" id="hide" >
                                <label>Designation :</label>
                                <designationDropdown v-model="designation" :isMultiple="true" @update:modelValue="GetFilter('designation')" />
                            </div>
                            <div class="col-md-3 col-lg-3  col-12 form-group" id="hide" v-if="advanceFilters">
                                    <label>Employee Type :</label>
                                    <multiselect  @update:modelValue="GetFilter('employeeType')" v-model="employeeType" :options="['Permanent', 'Probation', 'Internee', 'Temporary']" v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'" :show-labels="false" :placeholder="$t('AddEmployeeRegistration.SelectEmployeeType')">
                                                    </multiselect>
                    </div>
                    <div class=" col-lg-3 mt-1" >

<button v-on:click="search22(true)" :disabled ="!isFilterButtonDisabled" type="button" class="btn btn-outline-primary mt-3">
    {{ $t('Sale.ApplyFilter') }}
</button>
<button v-on:click="clearData(false)" :disabled ="!isFilterButtonDisabled" type="button" class="btn btn-outline-primary mx-2 mt-3">
    {{ $t('Sale.ClearFilter') }}
</button>

</div> 
                           
                    </div>
                    <!-- <div class="input-group">
                        <button class="btn btn-secondary" type="button" id="button-addon1">
                            <i class="fas fa-search"></i>
                        </button>
                        <input v-model="search" type="text" class="form-control" :placeholder="$t('EmployeeRegistration.SearchbyBrand')"
                               aria-label="Example text with button addon" aria-describedby="button-addon1">
                    </div> -->
                 
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>
                                        {{ $t('EmployeeRegistration.CODE') }}
                                    </th>
                                    <th v-if="english=='true'">
                                        {{$englishLanguage($t('EmployeeRegistration.ENGLISHNAME'))  }}
                                    </th>
                                    <th v-if="isOtherLang()">
                                        {{$arabicLanguage($t('EmployeeRegistration.ARABICNAME'))  }}
                                    </th>
                                    <th>
                                        {{ $t('EmployeeRegistration.REGISTRATIONDATE') }}
                                    </th>
                                    <th>
                                        {{ $t('EmployeeRegistration.GENDER') }}
                                    </th>
                                    <th>
                                        {{ $t('EmployeeRegistration.IDNO') }}
                                    </th>
                                    <th>
                                        {{ $t('EmployeeRegistration.Active') }}
                                    </th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(employee) in employeelist" v-bind:key="employee.id">
                                    <td v-if="isValid('CanEditEmployeeReg')">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditEmployee(employee.id)">{{employee.code}}</a>
                                        </strong>
                                    </td>
                                    <td v-else>
                                        {{employee.code}}
                                    </td>

                                    <td v-if="english=='true'">
                                        {{employee.englishName}}
                                    </td>
                                    <td v-if="isOtherLang()">
                                        {{employee.arabicName}}
                                    </td>

                                    <td>
                                        {{convertDate(employee.registrationDate)}}
                                    </td>


                                    <td>
                                        {{employee.gender}}
                                    </td>
                                    <td>
                                        {{employee.idNumber}}
                                    </td>
                                    <td v-if="employee.isActive" class="text-right d-flex justify-content-right">
                                        <button class="btn btn-sm      ml-1 mr-1 " v-on:click="ViewEmployee(employee.id)"  title="Permission">
                                            <i class="fa fa-eye"></i>
                                        </button>
                                        <div class="form-check form-switch pt-1" >
                                            <input class="form-check-input" v-on:change="EditEmployeeStatus(employee.id, true)"    type="checkbox">
                                        </div>

                                    </td>
                                    <td v-else class="text-right d-flex justify-content-right">
                                        <button class="btn btn-sm      ml-1 mr-1 " v-on:click="ViewEmployee(employee.id)"  title="Permission">
                                            <i class="fa fa-eye"></i>
                                        </button>
                                        <div class="form-check form-switch pt-1" >
                                            <input class="form-check-input" v-on:change="EditEmployeeStatus(employee.id, false)" checked   type="checkbox">
                                        </div>

                                    </td>
                                    <td>
                                        <a v-if="isValid('LeaveManagement') " v-on:click="EmployeeRegistration(employee.id,true)"
                                        href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                            <i class="align-self-center icon-xs ti-plus"></i>
                                            Create User Emails
                                        </a>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <hr />
                    <div class="float-start">
                        <span v-if="currentPage === 1 && rowCount === 0"> {{ $t('Pagination.ShowingEntries') }}</span>
                        <span v-else-if="currentPage === 1 && rowCount < 10">
                            {{ $t('Pagination.Showing') }}
                            {{ currentPage }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                            {{ rowCount }} {{ $t('Pagination.entries') }}
                        </span>
                        <span v-else-if="currentPage === 1 && rowCount >= 11">
                            {{ $t('Pagination.Showing') }}
                            {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{ $t('Pagination.of') }}
                            {{ rowCount }} {{ $t('Pagination.entries') }}
                        </span>
                        <span v-else-if="currentPage === 1">
                            {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                                $t('Pagination.to')
                            }} {{ currentPage * 10 }} of {{ rowCount }} {{ $t('Pagination.entries') }}
                        </span>
                        <span v-else-if="currentPage !== 1 && currentPage !== pageCount">
                            {{ $t('Pagination.Showing') }}
                            {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{
                                    $t('Pagination.of')
                            }} {{ rowCount }} {{ $t('Pagination.entries') }}
                        </span>
                        <span v-else-if="currentPage === pageCount">
                            {{ $t('Pagination.Showing') }}
                            {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ rowCount }} {{
 $t('Pagination.of')
                            }}
                            {{ rowCount }} {{ $t('Pagination.entries') }}
                        </span>
                    </div>
                    <div class="float-end">
                        <div class="overflow-auto" v-on:click="getPage()">
                            <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount" :per-page="10"
                                          first-text="First" prev-text="Previous" next-text="Next" last-text="Last">
                            </b-pagination>
                        </div>
                    </div>

                </div>
            </div>

             <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>
        <quickemployeemodel :newEmployee="newEmployee"
                            :show="show"
                            v-if="show"
                            @close="show = false"
                            :type="type" />
    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>

<script>
    import moment from 'moment';
    import clickMixin from '@/Mixins/clickMixin'
    import Multiselect from 'vue-multiselect';
    //import { Status } from 'filepond';
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        mixins: [clickMixin],
        components: {
            Multiselect,
            Loading
        },
        data: function () {
            return {
                  loading: false,
                arabic: '',
                employeeType: '',
                department: '',
                advanceFilters: false,
                designation: '',
                english: '',
                searchQuery: '',
                employeelist: [],
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                search: '',
                show: false,
                newEmployee: {
                    id: '00000000-0000-0000-0000-000000000000',
                    code: '',
                    registrationDate: '',
                    englishName: '',
                    arabicName: '',
                    gender: '',
                    idNumber: '',
                    email: ''
                },
                type: '',
                employee: {
                    id: '',
                    isActive: ''
                },
            }
        },
      
        watch: {
            // search: function (val) {
            //     this.GetEmployeeData(val, 1);
            // }
        },
        computed: {
            // employeeList: function () {
            //     return this.employeelist;

            // },
            isFilterButtonDisabled() {
      return (
        this.employeeType ||
        this.designation ||
        this.department 
      );
    },
        },
        methods: {

            search22: function () {
            this.GetEmployeeData(this.search, this.currentPage,this.employeeType,this.department,this.designation);
        },

        clearData: function () {
            this.search = "";
            this.GetEmployeeData(this.search, this.currentPage,this.employeeType,this.department,this.designation);

        },

            EmployeeRegistration: function (val,val2) {
                  this.loading = true;
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Hr/EmployeeToUser?employeeId=' + val +'&individual=' + val2, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data.isAddUpdate != null) {
                        root.$swal({
                            title: "Saved!",
                            text: "Data has been added successfully",
                            type: 'success',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });
                        root.close();
                    }
                    else {
                        root.$swal({
                            title: "Error!",
                            text: "Your Color Name  Already Exist!",
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });
                    }
                }).catch(error => {
                    console.log(error)
                    root.$swal.fire(
                        {
                            icon: 'error',
                            title: 'Something Went Wrong!',
                            text: error.response.data,

                            showConfirmButton: false,
                            timer: 5000,
                            timerProgressBar: true,
                        });

                    root.loading = false
                })
                .finally(() => root.loading = false);
        },

            GetFilter: function () {
               
               this.GetEmployeeData(this.search, this.currentPage);
            },
            FilterWareHouse: function () {
                

            this.advanceFilters = !this.advanceFilters;
            if (this.advanceFilters == false) {
               
               this.department='';
               this.designation='';
               this.employeeType='';

            }
            this.GetEmployeeData(this.search, this.currentPage);


        },
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            openmodel: function () {
                this.newEmployee = {
                    id: '00000000-0000-0000-0000-000000000000',
                    code: '',
                    registrationDate: '',
                    englishName: '',
                    arabicName: '',
                    gender: '',
                    idNumber: '',
                    email: ''

                }
                this.show = !this.show;
                this.type = "Add";
            },
            convertDate: function (date) {
                return moment(date).format('DD MMM YYYY');
            },
            getPage: function () {
                this.GetEmployeeData(this.search, this.currentPage);
            },
            GetEmployeeData: function (search, currentPage) {
                
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('EmployeeRegistration/EmployeeList?searchTerm=' + search + '&pageNumber=' + currentPage+ '&departmentId=' + this.department+ '&designationId=' + this.designation+ '&employeeType=' + this.employeeType, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.employeelist = response.data.results;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                    }
                });
            },
            AddEmployee: function () {
                var root = this;
           //     this.$router.push('/addEmployeeRegistration')
                root.$router.push({
                                path: '/addEmployeeRegistration',
                                query: {
                                Add: true,
                                }
                            })
            },
            EditEmployee: function (Id) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/EmployeeRegistration/EmployeeDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/addEmployeeRegistration',
                                query: {
                                    data: '',
                                    Add: false,
                                }
                            })
                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });

            },
            ViewEmployee: function (Id) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/EmployeeRegistration/EmployeeDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/EmployeeView',
                                query: {
                                    data: '',
                                    Add: false,
                                }
                            })
                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });

            },
            EditEmployeeStatus: function (Id, isActive) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                this.employee.id = Id;
                this.employee.isActive = !isActive;

                root.$https.post('/EmployeeRegistration/SaveEmployeeStatus' , root.employee, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != '00000000-0000-0000-0000-000000000000') {
                            root.getPage();
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
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.GetEmployeeData(this.search, 1);
        }
    }
</script>