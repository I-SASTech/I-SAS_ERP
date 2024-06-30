<template>
    <div class="col-md-12 ml-auto mr-auto">
        <div class="row">
            <div class="col-lg-12 ">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="col d-flex align-items-baseline">
                            <div class="media">
                                <span class="circle-singleline" style="background-color: #1761FD !important;">CP</span>
                                <div class="media-body align-self-center ms-3">
                                    <h4 class="mb-0" v-if="process.id === '00000000-0000-0000-0000-000000000000'">{{
                                        $t('AddProcess.CreateProcess') }} <span class="ps-2 non-bold-text">{{ process.date
    }}</span></h4>
                                    <h4 class="mb-0" v-else>{{ $t('AddProcess.UpdateProcess') }} <span
                                            class="ps-2 non-bold-text">{{ process.date }}</span></h4>
                                    <div class="col d-flex ">
                                        <p class="text-muted mb-0" style="font-size:13px !important;"><b>{{ process.code
                                        }}</b></p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <hr class="hr-dashed hr-menu my-0" />

        <div class="card border-0">
            <div class="card-body border-0">
                <div class="col-lg-12">
                    <div class="row">
                        <div class="col-md-6 mt-2">
                            <label>{{ $t('AddProcess.ProcessName') }} <span class="text-danger"> *</span></label>
                            <input class="form-control" v-model="v$.process.englishName.$model" type="text" />
                            <span v-if="v$.process.englishName.$error" class="error">
                            </span>
                        </div>
                        <div class="col-md-6 mt-2">
                            <label>{{ $t('AddProcess.Color') }} <span class="text-danger"> *</span></label>
                            <input class="form-control" v-model="process.color" type="text" @focus="showcolors = true"
                                @blur="showcolors = false" />
                        </div>
                        <div class="col-md-6 mt-2">
                            <label>{{ $t('AddProcess.Description') }} </label>
                            <input class="form-control" v-model="process.description" type="text" />
                        </div>
                        <div class="col-md-6 mt-2">
                            <label>{{ $t('AddProcess.SelectContractors') }}:</label>
                            <multiselect v-model="processContractors" :options="options" :multiple="true" track-by="name"
                                :clear-on-select="false" :show-labels="false" label="name"
                                v-bind:placeholder="$t('SelectOption')"
                                @update:modelValue="contractorListId(processContractors)">

                            </multiselect>

                        </div>
                    </div>

                        
                </div>
                <br />
                <process-item @update:modelValue="SaveProcessItems" />

                <div v-if="!loading" class=" col-lg-12 invoice-btn-fixed-bottom">
                    <div v-if="process.id === '00000000-0000-0000-0000-000000000000'">
                        <button class="btn btn-outline-primary  me-2" v-on:click="SaveProcess('Draft')" :disabled="v$.process.$invalid">
                            <i class="far fa-save"></i> {{ $t('AddProcess.SaveAsDraft') }}
                        </button>
                        <button class="btn btn-outline-primary  me-2" v-on:click="SaveProcess('Approved')" :disabled="v$.process.$invalid">
                            <i class="far fa-save"></i> {{ $t('AddProcess.SaveAsPost') }}
                        </button>
                        <button class="btn btn-danger  me-2" v-on:click="close">
                            {{ $t('AddProcess.Cancel') }}
                        </button>
                    </div>
                    <div v-else>
                        <button class="btn btn-outline-primary  me-2" v-on:click="SaveProcess('Draft')" :disabled="v$.process.$invalid">
                            <i class="far fa-save"></i> {{ $t('AddProcess.UpdateAsDraft') }}
                        </button>
                        <button class="btn btn-outline-primary  me-2" v-on:click="SaveProcess('Approved')"
                            :disabled="v$.process.$invalid || process.processItems.filter(x => x.quantity == '').length > 0">
                            <i class="far fa-save"></i> {{ $t('AddProcess.UpdateAsPost') }}
                        </button>
                        <button class="btn btn-danger  me-2" v-on:click="close">
                            {{ $t('AddProcess.Cancel') }}
                        </button>
                    </div>
                </div>
            </div>
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>
    </div>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import moment from "moment";
import { maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
/*import { Sketch } from 'vue-color'*/
import Multiselect from 'vue-multiselect';
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/css/index.css';
export default {
    mixins: [clickMixin],
    props: ['type'],
    components: {
            /*'sketch-picker': Sketch,*/ Multiselect,
        Loading
    },
    setup() {
        return { v$: useVuelidate() }
    },
    data: function () {
        return {
            currency: '',
            process: {
                id: '00000000-0000-0000-0000-000000000000',
                code: '',
                englishName: '',
                arabicName: '',
                color: '',
                date: '',
                processItems: [],
                contractorId: [],
                processContractors: [],
                description: '',

            },
            contractorId: [],
            processContractors: [],

            color: '',
            arabic: '',
            english: '',
            render: 0,
            daterander: 0,
            loading: false,
            showcolors: false,
            options: [],
            employeeType: 'Contractor',
            DisplayValue: [],
            isUpdate: false



        }
    },
    validations() {
        return {
            process: {
                code: {

                    maxLength: maxLength(30)
                },
                englishName: {
                    maxLength: maxLength(30)
                },
                arabicName: {
                    maxLength: maxLength(30)
                },
                processItems: {
                },
            }
        }
    },
    watch: {
        color: function (val) {

            this.process.color = val.hex;
            this.showcolors = false;
        }
    },
    methods: {
        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        close: function () {
            this.$router.push('/Process');
        },
        SaveProcessItems: function (processItems) {


            this.process.processItems = processItems;
        },
        contractorListId: function (value) {

            var root = this;
            root.process.processContractors = [];
            value.forEach(function (val) {
                root.process.processContractors.push({
                    contractorId: val.id
                })
            });
        },
        GetAutoCodeGenerator: function () {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            this.$https.get('/Batch/ProcessAutoGenerateNo', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                if (response.data != null) {
                    root.process.code = response.data;
                }
            });
        },
        SaveProcess: function (status) {

            this.process.approvalStatus = status

            var root = this;
            this.loading = true;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.$https.post('/Batch/SaveProcessInformation', this.process, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data.isSuccess == true) {
                        if (root.type != "Edit") {

                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully!' : '!حفظ بنجاح',
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
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update Successfully!' : 'تم التحديث بنجاح',
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.close();

                        }
                    }
                    else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: "Your Sample Request Name  Already Exist!",
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
        getEmployeeData: function (x, value) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.$https.get('/EmployeeRegistration/EmployeeList?IsDropDown=' + false + '&isSignup=' + true + '&employeeType=' + 'Contractor', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                if (response.data != null) {
                    response.data.results.forEach(function (result) {
                        root.options.push({
                            id: result.id,
                            name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (result.englishName != '' && result.englishName != null) ? result.code + ' ' + result.englishName : result.code + ' ' + result.arabicName : (result.arabicName != '' && result.arabicName != null) ? result.code + ' ' + result.arabicName : result.code + ' ' + result.englishName
                        })
                        if (x) {
                            if (value != undefined) {
                                value.forEach(function (ids) {

                                    if (ids.contractorId == result.id) {

                                        root.processContractors.push({
                                            id: result.id,
                                            name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (result.englishName != '' && result.englishName != null) ? result.code + ' ' + result.englishName : result.code + ' ' + result.arabicName : (result.arabicName != '' && result.arabicName != null) ? result.code + ' ' + result.arabicName : result.code + ' ' + result.englishName
                                        });
                                        root.contractorListId(root.processContractors);
                                    }
                                })

                            }
                        }

                    })
                }
            });
        },

    },
    created: function () {

        var root = this;

        if (root.$route.query.Add == 'false') {
            root.$route.query.data = this.$store.isGetEdit;
        }
    },
    mounted: function () {
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        this.currency = localStorage.getItem('currency');
        if (this.$route.query.data == undefined) {
            this.getEmployeeData(false);

            this.GetAutoCodeGenerator();
            this.process.date = moment().format("LLL");
        }
        else {


            this.process = this.$route.query.data;
            this.daterander++;
            this.getEmployeeData(true, this.process.processContractors);
            this.EmployeeData(this.process.processContractors);
            if (this.process.code != null && this.process.englishName != '' && this.process.color != '') {
               this.isUpdate = true;
            }
        }





    }
}
</script>
<style scoped>
.circle-singleline {
    margin: 20px;
    width: 60px;
    height: 60px;
    border-radius: 50%;
    font-size: 30px;
    text-align: center;
    background: blue;
    color: #fff;
    vertical-align: middle;
    line-height: 60px;
}

.non-bold-text {
    font-weight: normal;
    font-size: 14px !important;
}
</style>
