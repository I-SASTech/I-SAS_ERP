<template>
    <modal :show="show">

        <div style="margin-bottom:0px" class="card" >
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="modal-header">

                            <h5 class="modal-title" id="myModalLabel">{{ $t('RejectionModel.RejectedExpense') }}  </h5>

                        </div>
                        <div class="card">
                            <div class="card-body">
                                <div class="row ">
                                    <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.dailyExpense.reason.$error}">
                                        <label class="text  font-weight-bolder"> {{ $t('RejectionModel.Reason') }}: <span class="text-danger"> *</span></label>
                                        <textarea class="form-control"  v-model="v$.dailyExpense.reason.$model" type="text" />
                                        <span v-if="v$.dailyExpense.reason.$error" class="error">
                                            <span v-if="!v$.dailyExpense.reason.required">{{ $t('RejectionModel.NameRequired') }}</span>

                                        </span>
                                    </div>
                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                        <h6 class="label">{{ $t('RejectionModel.PleaseSelectYourPaymentType') }}</h6>
                                        <div class="form-check form-check-inline">

                                            <input class="form-check-input" v-model="dailyExpense.paymentType" type="radio" name="inlineRadioOptions" id="inlineRadio1" value="Cash">
                                            <label class="form-check-label" for="inlineRadio1">{{ $t('RejectionModel.Cash') }}</label>
                                        </div>
                                        <div class="form-check form-check-inline">
                                            <input class="form-check-input" type="radio" v-model="dailyExpense.paymentType" name="inlineRadioOptions" id="inlineRadio2" value="Credit">
                                            <label class="form-check-label" for="inlineRadio2">{{ $t('RejectionModel.Credit') }}</label>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer justify-content-right">

                            <button type="button" class="btn btn-primary  " v-on:click="SaveCity" v-bind:disabled="v$.dailyExpense.$invalid"> {{ $t('RejectionModel.btnSave') }}</button>
                            <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('RejectionModel.btnClear') }}</button>

                        </div>
                    </div>
                </div>
            </div>
             <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>

    </modal>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'

    export default {
        props: ['show', 'newdailyExpense'],
        mixins: [clickMixin],
          setup() {
            return { v$: useVuelidate() }
        },
         components: {
            Loading
        },
        data: function () {
            return {
                dailyExpense: this.newdailyExpense,
                render: 0,
                currency: '',
            }
        },
        validations() {
          return{
              dailyExpense: {
                reason: {
                    required,
                },
                paymentType: {
                    required
                },

            }
          }
        },
        methods: {
            close: function () {
                this.$emit('close');
            },
            SaveCity: function () {
                this.loading = true;
                var token = '';
                var root = this;
                var url = '/Company/SaveDailyExpense';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https
                    .post(url, root.dailyExpense, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        if (response.data != null) {
                            this.$swal.fire({
                                title: root.$t('RejectionModel.SavedSuccessfully'),
                                text: root.$t('RejectionModelSaved'),
                                type: 'success',
                                confirmButtonClass: "btn btn-success",
                                buttonStyling: false,
                                icon: 'success',
                                timer: 1500,
                                timerProgressBar: true,

                            });
                            root.close();
                            this.$router.push('/dailyexpense')
                        }
                    })
                    .catch(error => {

                        console.log(error)
                        this.$swal.fire(
                            {
                                icon: 'error',
                                title: this.$t('RejectionModel.Error'),
                                text: this.$t('RejectionModel.Error'),
                            });
                        root.errored = true
                    })
                    .finally(() => root.loading = false)
            }
        },
        mounted: function () {
            this.currency = localStorage.getItem('currency');


        }
    }
</script>
