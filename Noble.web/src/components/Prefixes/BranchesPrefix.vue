<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">Branch Prefix</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Prefixies.Home') }}</a></li>
                                    <li class="breadcrumb-item active">Branch Prefix</li>
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
                            <div class="row form-group">
                                <label class="col-form-label col-lg-4">
                                    <span class="tooltip-container text-dashed-underline ">Branch Prefixes:</span>
                                </label>
                                <div class="inline-fields col-lg-8">
                                    <input class="form-control" type="text" v-model="v$.prefix.prefix.$model">
                                    <span v-if="v$.prefix.prefix.$error" class="error">
                                        <span v-if="!v$.prefix.prefix.maxLength">Length not greater than 1</span>
                                    </span>
                                </div>
                            </div>
                            
                            <div class="row form-group">
                                <label class="col-form-label col-lg-4">
                                    <span class="tooltip-container text-dashed-underline ">Starting Number:</span>
                                </label>
                                <div class="inline-fields col-lg-8">
                                    <input class="form-control" type="text" v-model="prefix.startingNumber">
                                  
                                </div>
                            </div>
                            
                        </div>
                        <div class="col-lg-6">
                            
                            <button type="button" class="btn btn-outline-primary ms-5 me-2" :disabled="v$.prefix.$invalid" v-on:click="ChangePrefixes">Change Prefixes </button>

                           
                        </div>
                    </div>
                </div>
                <div class="card-footer">
                    <button type="button" class="btn btn-outline-primary ms-5 me-2" :disabled="v$.prefix.$invalid" v-on:click="SaveSetting"><i class="far fa-save" ></i>{{ $t('Prefixies.Save') }} </button>
                    <button type="button" class="btn btn-outline-danger" v-on:click="GotoPage('/Branches')">{{ $t('Prefixies.Cancel') }}</button>
                </div>
            </div>
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>
    </div>
</template>
 <script>
    
    import { required,maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    import clickMixin from '@/Mixins/clickMixin'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default ({
        name: "Prefixies",
         mixins: [clickMixin],
         components: {
            Loading
        },
         setup() {
             return { v$: useVuelidate() }
         },
        data: function () {
            return {
                prefix: {
                    prefix: '',
                    startingNumber: '',
                    endNumber: '',
                    locationId: '',
                   
                }
            }
        },
         validations() {
             return {
                 prefix: {
                     prefix: {
                         required,
                         maxLength: maxLength(1)
                     },


                     startingNumber: {
                     },
                 }
                 }
        },
        methods: {
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            ChangePrefixes: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Branches/GetBranchAutoCode?changePrefixes=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    

                    if (response.data == '') {
                        root.$swal({
                                title: "Changed!",
                                text: "Changed Successfully!",
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                    }
                });
            },
            SaveSetting: function () {
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                this.$https.post('/Branches/SaveBranchPrefix', this.prefix, { headers: { "Authorization": `Bearer ${token}` } })
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
                this.$https.get('/Branches/BranchPrefixDetail', { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.prefix = response.data;
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
            this.prefix.locationId=  localStorage.getItem('CompanyID');

        }
    })
</script> 