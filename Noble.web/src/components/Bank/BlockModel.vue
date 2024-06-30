<template>
    <modal :show="show" >
        <div style="margin-bottom:0px" class="card" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            <div class="row">
                                <div class="col-xs-12 col-sm-8 col-md-8 col-lg-8">
                                    <div>
                                        <span class="Heading1">{{ $t('ChequeBook.ChequeBook') }}</span>
                                    </div>

                                </div>
                               


                            </div>

                            <div class="row ">
                                <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.reason.$error}">
                                    <label class="text  font-weight-bolder"> {{ $t('ChequeBook.Reason') }}:<span class="text-danger"> *</span> </label>
                                    <textarea class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.reason.$model" type="text" />
                                </div>

                             

                              
                            </div>
                        </div>

                        <div v-if="!loading">
                            <div class="modal-footer justify-content-right">
                                <button type="button" class="btn btn-primary  " v-on:click="BlockCheque" v-bind:disabled="v$.reason.$invalid"> {{ $t('ChequeBook.Save') }}</button>
                                <button type="button" class="btn btn-danger  mr-3 " v-on:click="close(false)">{{ $t('ChequeBook.Cancel') }}</button>
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
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
     import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

    export default {
        mixins: [clickMixin],
        props: ['show','id'],
        components: {
        Loading   
        },
             setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {

                reason: '',
                currency: '',
                arabic: '',
                english: '',
                render: 0,
                dateRender: 0,
                loading: false,
            }
        },
        validations() {
           
              return{
                  reason: {
                    required
                },
                
              }

        },
        methods: {
            close: function (x) {
                
                if (x == true)
                    this.$emit('close', x);
                else {
                    this.$emit('close', false);
                }

            },
            BlockCheque: function () {


                var root = this;
                // working with IE and Chrome both
                this.$swal({
                    title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Are you sure?' : 'هل أنت متأكد؟', 
                    text: "Are You sure you want to block Cheque Book !",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Yes, delete it!' : 'نعم ، احذفها!', 
                    closeOnConfirm: false,
                    closeOnCancel: true
                }).then(function (result) {
                    if (result.isConfirmed) {
                        
                        var token = '';
                        if (token == '') {
                            token = localStorage.getItem('token');
                        }
                        root.$https.get('/Payroll/BlockChequeBook?Id=' + root.id + '&reason=' + root.reason, { headers: { "Authorization": `Bearer ${token}` } })
                            .then(function (response) {
                                if (response.data != null) {


                                    root.$swal({
                                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Deleted!' : 'تم الحذف!',
                                        text: 'Cheque Book Blocked!',
                                        type: 'success',
                                        confirmButtonClass: "btn btn-success",
                                        buttonsStyling: false
                                    });
                                    root.close(true);
                                }
                            },
                                function () {

                                    root.$swal({
                                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                        text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                        type: 'error',
                                        confirmButtonClass: "btn btn-danger",
                                        buttonsStyling: false
                                    });
                                });
                    }
                    else {
                        this.$swal((this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Cancelled!' : 'ألغيت!', (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Your file is still intact!' : 'ملفك لا يزال سليما!', (this.$i18n.locale == 'en' || root.isLeftToRight()) ? 'info' : 'معلومات');
                    }
                });
            },
        },
        mounted: function () {
          


            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.currency = localStorage.getItem('currency');



        }
    }
</script>
<style scoped>
    .input-group-append .input-group-text, .input-group-prepend .input-group-text {
        background-color: #e3ebf1;
        border: 1px solid #e3ebf1;
        color: #000000;
    }

    .input-group .form-control {
        border-left: 1px solid #e3ebf1;
    }

        .input-group .form-control:focus {
            border-left: 1px solid #3178F6;
        }

    .input-group-text {
        border-radius: 0;
    }

    .Heading1 {
        font-size: 27px !important;
        font-style: normal;
        font-weight: 600;
        color: #3178F6;
    }

    .Heading2 {
        font-size: 20px !important;
        font-style: normal;
        color: black;
    }

    .SpanColor {
        font-size: 15px !important;
        font-style: normal;
        font-weight: 600;
    }
</style>


