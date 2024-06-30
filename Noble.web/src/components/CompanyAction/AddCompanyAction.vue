<template>
    <modal :show="show" v-if=" isValid('CanAddOrderAction')  ">

        <div style="margin-bottom:0px" class="card" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">                        
                        <div>
                            <div class="card-body ">
                                <div class="row ">
                                    <div class="form-group has-label col-sm-12 " v-bind:class=" ($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                        <label class="text  font-weight-bolder"> {{ $t('AddCompanyAction.Type') }}:</label>
                                        <companyprocessdropdown v-model="newAction.processId" :documenttype="document" />
                                    </div>

                                    <div class="form-group has-label col-sm-12 " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                        <label class="text  font-weight-bolder"> {{ $t('AddCompanyAction.Date') }}: </label>
                                        <datepicker v-model="newAction.date" />
                                    </div>

                                    <div class="form-group has-label col-sm-12 " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                        <label class="text  font-weight-bolder"> {{ $t('AddCompanyAction.description') }}: </label>
                                        <textarea class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="newAction.description" type="text" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div v-if="!loading">
                            <div class="modal-footer justify-content-right">
                                <button type="button" class="btn btn-primary  " v-on:click="SaveProcess" v-bind:disabled="v$.newAction.$invalid"> {{ $t('AddCompanyAction.btnSave') }}</button>
                                <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('AddCompanyAction.btnClear') }}</button>
                            </div>
                        </div>
                        <div v-else>
                            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </modal>
    <acessdenied v-else :model=true></acessdenied>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
      import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    export default {
        props: ['show', 'action','document'],
        mixins: [clickMixin],
        components: {
           Loading 
        },
         setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                arabic: '',
                english: '',
                render: 0,
                loading: false,
                processList:[],
                newAction: this.action,
            }
        },
        validations() {
          return{
              newAction: {
                purchaseOrderId: {
                    required
                },
                processId: {
                    required
                }
            }
          }
        },
        methods: {            
            close: function () {
                this.$emit('close');
            },
            SaveProcess: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/Purchase/SavePurchaseOrderAction', this.newAction, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                    if (response.data.isSuccess == true) {                        
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
                    }).catch(error => {
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
                    .finally(() => root.loading = false)
            }
        },
        mounted: function () {
            
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
        }
    }
</script>
