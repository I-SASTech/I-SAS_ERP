<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">Transaction/Application Log</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Prefixies.Home') }}</a></li>
                                    <li class="breadcrumb-item active">Transaction/Application Log</li>
                                </ol>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card col-md-12">

                <div class="card-body">
                    <div class="row">
                        
                        <div class="col-lg-6">
                            <label class=" form-label align-self-center pt-3">Fresh Log Moving Days:</label>
                        <multiselect v-model="freshLog" @update:modelValue="GetValue('FreshLog')" :options="option1"  track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="false" placeholder="Select Type">
                        </multiselect>
                        </div>
                        <div class="col-lg-6">
                            <label class=" form-label align-self-center pt-3">Delete From History:</label>
                        <multiselect v-model="deleteHistory" @update:modelValue="GetValue('DeleteHistory')"  :options="option1"  track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="false" placeholder="Select Type">
                        </multiselect>
                        </div>
                </div>
                <div class="card-footer">
                    <button type="button" class="btn btn-outline-primary ms-5 me-2" :disabled="v$.log.$invalid" v-on:click="SaveSetting"><i class="far fa-save" ></i>{{ $t('Prefixies.Save') }} </button>
                    <button type="button" class="btn btn-outline-danger" v-on:click="GotoPage('/StartScreen')">{{ $t('Prefixies.Cancel') }}</button>
                </div>
            </div>

        </div>
        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>
</div>
</template>
 <script>
    import Multiselect from 'vue-multiselect'
    import moment from 'moment'

    
    import { required,minValue} from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';

    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import clickMixin from '@/Mixins/clickMixin'
    export default ({
        name: "Prefixies",
        mixins: [clickMixin],
        components: {
            Multiselect,
            Loading
         },
         setup() {
             return { v$: useVuelidate() }
         },
        data: function () {
            return {
                loading: false,
                options2:[{ value: 7, name: '7 Days' },{ value:15, name: '15 Days' },{ value: 30, name: '30 Days' },{ value: 60, name: '2 Months' }],
                option1: [{ value: 7, name: '7 Days' },{ value:15, name: '15 Days' },{ value: 30, name: '30 Days' },{ value: 60, name: '2 Months' }],
                freshLog:'',
                deleteHistory:'',
                log: {
                    freshLogMovingDays: 0,
                    deleteFromHistory: 0,
                    date:moment().format('llll'),
                    locationId: localStorage.getItem('CompanyID'),
                    IsActive: true,
                   
                }
            }
        },
         validations() {
             return {
                 log: {
                     freshLogMovingDays: {
                         required,
                         minValue: minValue(0.01)
                     },


                     deleteFromHistory: {
                         required,
                         minValue: minValue(0.01)

                     },
                 }
                 }
        },
        methods: {
            GetValue: function (value) {
                
                if(value=='FreshLog')
                {
                    if(this.freshLog.name!=null && this.freshLog.name!='' && this.freshLog.name!=undefined  )
                    {
                        this.log.freshLogMovingDays=this.freshLog.value;

                    }
                }
                if(value=='DeleteHistory')
                {
                    if(this.deleteHistory.name!=null && this.deleteHistory.name!='' && this.deleteHistory.name!=undefined  )
                    {
                        this.log.deleteFromHistory=this.deleteHistory.value;

                    }
                }
            },
            SaveSetting: function () {
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                this.$https.post('/Branches/SaveTransactionLog', this.log, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.$swal({
                                title: "Saved!",
                                text: "Saved Successfully!",
                                type: 'success',
                                icon: 'success',
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
                    .finally(() => root.loading = false)
            },

            GetData: function () {
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Branches/GetTransactionLogDetail?locationId='+localStorage.getItem('CompanyID'), { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        
                        if (response.data != null && response.data != '' ) {
                            root.log = response.data;
                            
                            root.freshLog=root.option1.find(x=>x.value==response.data.freshLogMovingDays);
                            root.deleteHistory=root.option1.find(x=>x.value==response.data.deleteFromHistory);
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
                    .finally(() => root.loading = false)
            },
        },
        created: function () {
            this.GetData()

        },
        mounted: function () {

        }
    })
</script> 