<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('AddShiftAssign.AddShiftAssign') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('AddShiftAssign.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('AddShiftAssign.AddShiftAssign') }}</li>
                                </ol>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-lg-3 form-group">
                            <label>{{ $t('AddShiftAssign.ShiftName') }}</label>
                            <shift-dropdown v-model="newShiftAssign.shiftId" :modelValue="newShiftAssign.shiftId" />
                        </div>
                        <div class="col-lg-3 form-group">
                            <label>{{ $t('AddShiftAssign.ShiftManager') }}</label>
                            <employeeDropdown v-model="newShiftAssign.employeeId" :modelValue="newShiftAssign.employeeId" />
                        </div>
                        <div class="col-lg-3 form-group" v-if="newShiftAssign.id!='00000000-0000-0000-0000-000000000000'">
                            <div class="checkbox checkbox-success form-check-inline mt-4">
                                <input type="checkbox" id="inlineCheckbox2" v-model="newShiftAssign.isActive">
                                <label for="inlineCheckbox2">{{ $t('AddShiftAssign.Status') }}  </label>
                            </div>
                        </div>
                        <div class="col-lg-3 form-group" v-if="!newShiftAssign.isActive">
                            <label>{{ $t('AddShiftAssign.ReasonOfClosing') }}</label>
                            <textarea class="form-control" v-model="newShiftAssign.reasonOfClosingShift" />
                        </div>
                        <div class="col-lg-12 form-group">
                            <label>{{ $t('AddShiftAssign.Description') }}  </label>
                            <textarea class="form-control" v-model="newShiftAssign.description" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="card  mb-5">
                <div class="card-header">
                    <div class="row">
                        <h5 class="page-title">{{ $t('AddShiftAssign.Filters') }}</h5>
                        <div class="col-lg-4">
                            <label>{{ $t('AddShiftAssign.Designation') }}</label>
                            <designation-multi-dropdown v-model="designationId" />
                        </div>
                        <div class="col-lg-4">
                            <label>{{ $t('AddShiftAssign.Department') }}</label>
                            <department-multi-dropdown v-model="departmentId" />
                        </div>
                        <div class="col-lg-4">
                            <label>Employee</label>
                            <employeeDropdown v-model="employeeId" @update:modelValue="GetEmployeeDetail(employeeId)" />
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th style="width:5%;">#</th>
                                    <th style="width:25%;">
                                        {{ $t('AddShiftAssign.EmployeeName') }}
                                    </th>

                                    <th style="width:25%;">
                                        {{ $t('AddShiftAssign.Description') }}
                                    </th>
                                    <th style="width:10%;" class="text-center">
                                        {{ $t('ShiftAssign.Status') }}
                                    </th>
                                    <th class="text-center" style="width:25%;">
                                        {{ $t('AddShiftAssign.ReasonOfClosing') }}
                                    </th>

                                    <th style="width:10%;">

                                    </th>

                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(shift ,index) in newShiftAssign.shiftEmployeeAssigns" v-bind:key="index">
                                    <td v-if="currentPage === 1">
                                        {{index+1}}
                                    </td>
                                    <td v-else>
                                        {{((currentPage*10)-10) +(index+1)}}
                                    </td>

                                    <td>
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditShift(shift.id)">{{shift.employeeName}}</a>
                                        </strong>
                                    </td>
                                    <td>
                                        <input class="form-control" type="text" v-model="shift.description" />
                                    </td>

                                    <td class="text-center">
                                        <div v-if="newShiftAssign.id!='00000000-0000-0000-0000-000000000000'" class="checkbox checkbox-success form-check-inline">
                                            <input type="checkbox" :id="'inlineCheckbox2'+index" v-model="shift.isActive">
                                            <label :for="'inlineCheckbox2'+index">  </label>
                                        </div>
                                    </td>
                                    <td>
                                        <input v-if="!shift.isActive" class="form-control" type="text" v-model="shift.reasonOfClosingShift" />
                                    </td>
                                    <td class="text-end">
                                        <a @click="removeProduct(shift.employeeId)" href="javascript:void(0);"><i class="las la-trash-alt text-secondary font-16"></i></a>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>

            <div class="col-lg-12 invoice-btn-fixed-bottom">
                <div class="button-items">

                    <button class="btn btn-outline-primary  mr-2" v-on:click="SaveShift()"
                            v-bind:disabled="v$.$invalid ">
                        <i class="far fa-save"></i> {{ $t('AddShiftAssign.Save') }}
                    </button>

                    <button class="btn btn-danger  mr-2" v-on:click="GotoPage('/ShiftAssign')">
                        {{ $t('AddShiftAssign.Cancel') }}
                    </button>
                </div>
            </div>
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>
    </div>
</template>


<script>
    import clickMixin from '@/Mixins/clickMixin'
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    
    import moment from "moment";
    export default {
        mixins: [clickMixin],
        components: {
            Loading
        },
          setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                loading: false,
                currency: '',
                searchQuery: '',
                shiftList: [],
                newShift: {
                    id: '',
                    code: '',
                    startTime: '',
                    endTime: '',
                    description: '',
                    status: true,
                    shiftName: '',
                    nameArabic: '',
                    sunday: false,
                    monday: false,
                    tuesday: false,
                    wednesday: false,
                    thursday: false,
                    friday: false,
                    saturday: false,
                },
                search: '',
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                arabic: '',
                english: '',
                newShiftAssign: {
                    id: '00000000-0000-0000-0000-000000000000',
                    shiftId: '',
                    employeeId: '',
                    isActive: true,
                    description: '',
                    reasonOfClosingShift: '',
                    shiftEmployeeAssigns: [],
                },
                employeeId: '',
                designationId: '',
                departmentId: ''
            }
        },
        validations() {
        return{
                newShiftAssign:
            {
                shiftId: {
                    required
                },
                employeeId: {
                    required
                },
                shiftEmployeeAssigns: {
                    required
                },
            }
        }
        },

        watch: {
            search: function () {
                this.GetShiftData();
            },
            designationId: function () {
                this.GetEmployeeList();
            },
            departmentId: function () {
                this.GetEmployeeList();
            }
        },
        methods: {
            removeProduct: function (id) {
                this.newShiftAssign.shiftEmployeeAssigns = this.newShiftAssign.shiftEmployeeAssigns.filter((prod) => {
                    return prod.employeeId != id;
                });
            },

            getDate: function (date) {
                return moment(date).format('LL');
            },

            GotoPage: function (link) {
                this.$router.push({ path: link });
            },


            getPage: function () {
                this.GetShiftData();
            },


            getIds: function (value) {
                var sizeId = [];
                for (var i = 0; i < value.length; i++) {
                    sizeId[i] = value[i].id
                }
                return sizeId;
            },

            SaveShift: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/Hr/SaveShiftAssign', this.newShiftAssign, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isSuccess == true) {
                            if (root.newShiftAssign.id == "00000000-0000-0000-0000-000000000000") {

                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully!' : '!حفظ بنجاح',
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });

                                root.GotoPage('/ShiftAssign');
                            }
                            else {

                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update Successfully!' : 'تم التحديث بنجاح',
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });
                                root.GotoPage('/ShiftAssign');

                            }
                        }
                        else {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: response.data,
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error.response.data,

                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false);
            },


            GetEmployeeList: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var model = {
                    designationIds: '',
                    departmentIds: ''
                }

                if (this.designationId != null && this.designationId != undefined && this.designationId != '') {
                    model.designationIds = this.getIds(this.designationId);
                }

                if (this.departmentId != null && this.departmentId != undefined && this.departmentId != '') {
                    model.departmentIds = this.getIds(this.departmentId);
                }

                root.$https.post('Hr/SearchEmployeeList', model, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.newShiftAssign.shiftEmployeeAssigns = [];
                        response.data.results.forEach(function (cat) {
                            root.newShiftAssign.shiftEmployeeAssigns.push({
                                employeeId: cat.id,
                                employeeName: cat.englishName,
                                isActive: true,
                                description: '',
                                reasonOfClosingShift: '',
                            })
                        })

                        root.loading = false;
                    }
                    root.loading = false;
                });
            },

            GetEmployeeDetail: function (id) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var employee = root.newShiftAssign.shiftEmployeeAssigns.find((value) => value.employeeId == id);
                if (employee != undefined) {
                    root.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Warning!' : 'تحذير',
                        text: "Employee Already Added",
                        type: 'warning',
                        icon: 'warning',
                        showConfirmButton: false,
                        timer: 2000,
                        timerProgressBar: true,
                    });
                }
                else {
                    root.$https.get('/EmployeeRegistration/EmployeeDetail?id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(function (response) {
                            if (response.data != null) {
                                
                                root.newShiftAssign.shiftEmployeeAssigns.push({
                                    employeeId: response.data.id,
                                    employeeName: response.data.englishName,
                                    isActive: true,
                                    description: '',
                                    reasonOfClosingShift: '',
                                });
                            }
                        }, function (error) {
                            this.loading = false;
                            console.log(error);
                        });
                }

            },
        },

        created: function () {
            var root =  this;

            if(root.$route.query.Add == 'false')
            {
                root.$route.query.data = this.$store.isGetEdit;
            }
            this.$emit('update:modelValue', this.$route.name);
            
            if (this.$route.query.data != undefined) {
                if (this.$route.query.type != 'Duplicate') {
                    this.newShiftAssign.shiftId = this.$route.query.data.shiftId;
                    this.newShiftAssign.employeeId = this.$route.query.data.employeeId;
                    this.newShiftAssign.isActive = true;
                    this.newShiftAssign.shiftEmployeeAssigns = this.$route.query.data.shiftEmployeeAssigns;
                    this.newShiftAssign.shiftEmployeeAssigns.forEach(function (cat) {
                        cat.isActive = true;
                        cat.description = '';
                        cat.reasonOfClosingShift = '';
                    });
                }
                else {
                    this.newShiftAssign = this.$route.query.data;
                }
            }
        },

        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.currency = localStorage.getItem('currency');
            //this.GetShiftData();

        }
    }
</script>