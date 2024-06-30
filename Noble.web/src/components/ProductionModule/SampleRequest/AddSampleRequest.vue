<template>
    <div class="col-md-12 ml-auto mr-auto" >
        <div class="card" >
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="card-header p-0 row">
                            <div class="col-lg-9 " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'text-right'">
                                <span v-if="sampleRequest.id === '00000000-0000-0000-0000-000000000000'"> <span class="MainLightHeading">{{ $t('AddSampleRequest.CreateSampleRequest') }}- </span><span class="DayHeading">{{sampleRequest.code}}</span></span>
                                <span v-else><span class="MainLightHeading">{{ $t('AddSampleRequest.UpdateSampleRequest') }} - </span><span class="DayHeading">{{sampleRequest.code}}</span></span>

                            </div>
                            <div class="col-lg-3 " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                <span>
                                    {{sampleRequest.date}}
                                </span>
                            </div>
                        </div>

                        <div class="mt-3">
                            <div class="row" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'text-right'">

                                <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                                    <label>{{ $t('AddSampleRequest.Customer') }} :<span class="text-danger"> *</span></label>
                                    <customerdropdown v-model="sampleRequest.customerId" :modelValue="sampleRequest.customerId"></customerdropdown>
                                </div>                              


                                <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4" v-bind:class="{'has-danger' : v$.sampleRequest.noOfSampleRequests.$error} ">
                                    <label>{{ $t('AddSampleRequest.NoOfSamples') }}: <span class="text-danger"> *</span></label>
                                    <input class="form-control  " v-model="v$.sampleRequest.noOfSampleRequests.$model" type="text" />
                                    <span v-if="v$.sampleRequest.noOfSampleRequests.$error" class="error">
                                    </span>
                                </div>
                                <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4" v-bind:class="{'has-danger' : v$.sampleRequest.referredBy.$error} ">
                                    <label>{{ $t('AddSampleRequest.ReferredBy') }} : </label>
                                    <input class="form-control  " v-model="v$.sampleRequest.referredBy.$model" type="text" />
                                    <span v-if="v$.sampleRequest.referredBy.$error" class="error">
                                    </span>
                                </div>
                                <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                                    <label>{{ $t('AddSampleRequest.DueDate') }} : <span class="text-danger"> *</span></label>
                                    <datepicker v-model="sampleRequest.dueDate" :key="daterander" />
                                </div>
                            </div>
                        </div>
                        <br />
                        <sample-item @update:modelValue="SaveSampleItems" />

                    </div>
                    <div class="col-md-12 text-right">
                        <div v-if="sampleRequest.id === '00000000-0000-0000-0000-000000000000'">
                            <button class="btn btn-primary  mr-2"
                                    v-on:click="SaveSampleRequest('Draft')"
                                    :disabled="v$.$invalid || sampleRequest.sampleRequestItems.filter(x => x.quantity=='').length > 0">
                                <i class="far fa-save"></i>  {{ $t('AddSampleRequest.SaveAsDraft') }}
                            </button>
                            <button class="btn btn-primary  mr-2"
                                    v-on:click="SaveSampleRequest('Approved')"
                                    :disabled="v$.$invalid || sampleRequest.sampleRequestItems.filter(x => x.quantity=='').length > 0">
                                <i class="far fa-save"></i>  {{ $t('AddSampleRequest.SaveAsPost') }}
                            </button>
                            <button class="btn btn-danger  mr-2"
                                    v-on:click="close">
                                {{ $t('AddSampleRequest.Cancel') }}
                            </button>
                        </div>
                        <div v-else>
                            <button class="btn btn-primary  mr-2"
                                    v-on:click="SaveSampleRequest('Draft')"
                                    :disabled="v$.$invalid || sampleRequest.sampleRequestItems.filter(x => x.quantity=='').length > 0">
                                <i class="far fa-save"></i>  {{ $t('AddSampleRequest.UpdateAsDraft') }}
                            </button>

                            <button class="btn btn-primary  mr-2"
                                    v-on:click="SaveSampleRequest('Approved')"
                                    :disabled="v$.$invalid || sampleRequest.sampleRequestItems.filter(x => x.quantity=='').length > 0">
                                <i class="far fa-save"></i> {{ $t('AddSampleRequest.UpdateAsPost') }}
                            </button>
                            <button class="btn btn-danger  mr-2"
                                    v-on:click="close">
                                {{ $t('AddSampleRequest.Cancel') }}
                            </button>
                        </div>




                    </div>
                    <!--<div class="card-footer col-md-3" v-else>
                        <loading v-model:active="loading"
                                 :can-cancel="true"
                                 :on-cancel="onCancel"
                                 :is-full-page="true"></loading>
                                 
                    </div>-->
                    <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
                </div>
            </div>
        </div>
    </div>

</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";
    import { maxLength, required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        mixins: [clickMixin],
        props: [ 'type'],
        components: {
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                currency: '',
                sampleRequest: {
                    id: '00000000-0000-0000-0000-000000000000',
                    code: '',
                    noOfSampleRequests: '',
                    referredBy: '',
                    requestGenerated: '',
                    date: '',
                    dueDate: '',
                    customerId: '',
                    sampleRequestItems: []
                },
                arabic: '',
                english: '',
                render: 0,
                daterander: 0,
                loading: false,
            }
        },
        validations() {
            return {
                sampleRequest: {

                    customerId: {

                        required
                    },
                    code: {

                        maxLength: maxLength(30)
                    },
                    noOfSampleRequests: {

                        maxLength: maxLength(30)
                    },
                    referredBy: {

                    },
                    sampleRequestItems: {
                        required
                    },

                }
                }
        },
        methods: {
            close: function () {
                this.$router.push('/SampleRequest');
            },
            SaveSampleItems: function (sampleRequestItems) {

                this.sampleRequest.sampleRequestItems = sampleRequestItems;
            },
            GetAutoCodeGenerator: function () {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                this.$https.get('/Batch/SampleRequestAutoGenerateNo', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        root.sampleRequest.code = response.data;
                    }
                });
            },
            SaveSampleRequest: function (status) {
                
                this.sampleRequest.approvalStatus = status

                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/Batch/SaveSampleRequestInformation', this.sampleRequest, { headers: { "Authorization": `Bearer ${token}` } })
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
            }
        },

        created: function () {
            var root =  this;

if(root.$route.query.Add == 'false')
{
    root.$route.query.data = this.$store.isGetEdit;
}
            this.$emit('update:modelValue', this.$route.name);
        },

        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.currency = localStorage.getItem('currency');
            if (this.$route.query.data == undefined) {
                this.GetAutoCodeGenerator();
                this.sampleRequest.date = moment().format("LLL");
            }
            else {
                this.sampleRequest = this.$route.query.data;
                this.daterander++;
            }
           




        }
    }
</script>
