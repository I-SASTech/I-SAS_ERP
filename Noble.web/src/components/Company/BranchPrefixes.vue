<template>
    <modal :show="show" >
        <div class="modal-content">
           
            <div class="modal-header" >
             
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" >
                    Branch Prefix
                </h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            
            <div class="modal-body" >

                <div class="row">
                    <div  class="form-group has-label col-sm-12 " v-bind:class="{ 'has-danger': v$.prefix.prefix.$error }">
                        <label class="text  font-weight-bolder">Branch Prefixes:<span class="text-danger"> *</span></label>
                        <input class="form-control" type="text" v-model="v$.prefix.prefix.$model">
                        <span v-if="v$.prefix.prefix.$error" class="error">
                            <span v-if="!v$.prefix.prefix.maxLength">Length not greater than 1</span>
                        </span>
                    </div>
                    <div  class="form-group has-label col-sm-12 " v-bind:class="{ 'has-danger': v$.prefix.startingNumber.$error }">
                        <label class="text  font-weight-bolder">Starting Number:<span class="text-danger"> *</span></label>
                        <input class="form-control" type="text" v-model="v$.prefix.startingNumber.$model">
                        <span v-if="v$.prefix.startingNumber.$error" class="error">
                            <span v-if="!v$.prefix.startingNumber.maxLength">Length not greater than 1</span>
                        </span>
                    </div>
                   
                    
                   
                </div>
            </div>
            

            <div class="modal-footer" >
                <button type="button" class="btn btn-soft-primary btn-sm" :disabled="v$.prefix.$invalid" v-on:click="SaveSetting"  v-if="!list">
                    {{ $t('AddOrigin.btnSave') }}
                </button>
               
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">
                    {{
                            $t('AddOrigin.btnClear')
                    }}
                </button>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>
    </modal>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'

    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

import { required,maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
export default {
    mixins: [clickMixin],
    props: ['show','businessId','list'],
    components: {
        Loading
    },
    setup() {
            return { v$: useVuelidate() }
        },
    data: function () {
        return {
            resultQuery:[],
            prefix: {
                    prefix: '',
                    startingNumber: '',
                    endNumber: '',
                    locationId: '',
                   
                },
            render: 0,
            arabic: '',
            english: '',
            loading: false,
        }
    },
    validations() {
           return{
             prefix:{
                prefix:{
                    required,
                    maxLength:maxLength(1)
                },
              
               
                startingNumber:{
                },
            }
           }
        },
        methods: {
            close: function () {
            this.$emit('close');
        },
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
                
                this.prefix.locationId=this.businessId;
                
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
            GetBranch: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('Branches/BranchList?isDropdown=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        
                        root.resultQuery = response.data.results;
                    }
                });
            },

            GetData: function () {
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Branches/BranchPrefixDetail?branchId=' + this.businessId, { headers: { "Authorization": `Bearer ${token}` } })
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
            
            if(this.list)
            {
                this.GetBranch();

            }
            else
            {
                this.GetData();

            }

        },
        mounted: function () {
            this.prefix.locationId=  localStorage.getItem('CompanyID');

        }
   
    
}
</script>
