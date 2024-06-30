<template>
    <modal :show="show">

        <div style="margin-bottom:0px" class="card">
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="modal-header">
                            <h5 class="modal-title" id="myModalLabel"> {{$t('CashReceiving')}}</h5>
                        </div>


                        <div class="card-body">
                            <div class="row " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">

                                <div class="form-group has-label col-sm-12 ">
                                    <label class="text  font-weight-bolder">{{$t('ReceivedCash')}}:<span class="text-danger"> *</span> </label>
                                    <input disabled class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.receivingCashModel.amount.$model" type="number" />
                                </div>
                                <div class="form-group has-label col-sm-12 ">
                                    <label class="text  font-weight-bolder"> {{ $t('Password') }}:<span class="text-danger"> *</span> </label>
                                    <input class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.receivingCashModel.password.$model" type="password" />
                                </div>

                            </div>
                            <div class="modal-footer justify-content-right">

                                <button type="button" class="btn btn-primary  " v-on:click="SaveCashReceving()"> {{$t('CashReceiving')}}</button>
                                <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('Cancel') }}</button>

                            </div>
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
    export default {
        mixins: [clickMixin],

        props: ['paidAmountProp', 'show', 'inActiveDayId'],
          setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                receivingCashModel: {
                    id: '00000000-0000-0000-0000-000000000000',
                    reason: '',
                    amount: 0,
                    password: '',
                    userName: ''
                },

                paidAmount: 0

            }
        },
        validations() {
           return{
             receivingCashModel: {
                amount: {
                    required
                },
                password: {
                    required
                }
            }
           }
        },
        methods: {

            close: function (x) {
                if (x) {
                    this.$emit('close', true);

                }
                else {
                    this.$emit('close', false);

                }
            },
            SaveCashReceving: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                this.receivingCashModel.id = this.inActiveDayId == undefined ? '00000000-0000-0000-0000-000000000000' : this.inActiveDayId
                
                if (localStorage.getItem('iSupervisorLogin') == 'true') {
                    this.receivingCashModel.userName = localStorage.getItem('SupervisorUserName')
                }
                else {
                    this.receivingCashModel.userName = localStorage.getItem('UserName')
                }
               
                root.$https.post('/Product/ReceivingCash', root.receivingCashModel, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                        if (response.data != '00000000-0000-0000-0000-000000000000') {
                            if (response.data === 'Not Valid Credential') {
                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Please enter valid Credential' : 'الرجاء إدخال بيانات اعتماد صالحة',
                                    type: 'error',
                                    icon: 'error',
                                    showConfirmButton: false,
                                    timer: 2500,
                                    timerProgressBar: true,
                                });
                                root.close(false);
                            }
                            else {
                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Success!' : 'النجاح',
                                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update Successfully!' : 'التحديث بنجاح!',
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                }).then(function () {
                                    root.$router.go()
                                });
                                
                                //root.close(true);
                            }

                        }

                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هناك خطأ ما',
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                    });


            },
        },
        mounted: function () {
            this.paidAmount = this.paidAmountProp
            this.receivingCashModel.amount = this.paidAmountProp
        }
    }
</script>
