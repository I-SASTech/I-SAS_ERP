<template>
    <modal :show="show">
        <div style="margin-bottom:0px" class="card" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
            <div class="card-body">
                Not Working
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="modal-header" v-if="type=='Edit'">
                            <h5 class="modal-title DayHeading" id="myModalLabel">{{ $t('AddMonthlyCost.AddMonthlyCost') }}</h5>
                        </div>
                        <div class="modal-header" v-else>
                            <h5 class="modal-title DayHeading" id="myModalLabel">{{ $t('AddMonthlyCost.UpdateMonthlyCost') }}</h5>
                        </div>
                        <div class="text-left">
                            <div class="card-body">
                                <div class="row ">
                                    <monthlycostitem></monthlycostitem>



                                </div>
                            </div>
                        </div>
                        <div class="modal-footer justify-content-right" v-if="type=='Edit'">

                            <button type="button" class="btn btn-primary  " v-on:click="SaveMonthlyCost" v-bind:disabled="v$.monthlyCost.$invalid"><i class="far fa-save"></i>  {{ $t('AddMonthlyCost.btnUpdate') }}</button>
                            <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('AddMonthlyCost.btnClear') }}</button>

                        </div>
                        <div class="modal-footer justify-content-right" v-else>

                            <button type="button" class="btn btn-primary  " v-on:click="SaveMonthlyCost" v-bind:disabled="v$.monthlyCost.$invalid"><i class="far fa-save"></i>  {{ $t('AddMonthlyCost.btnSave') }}</button>
                            <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()"> {{ $t('AddMonthlyCost.btnClear') }}</button>

                        </div>
                    </div>
                </div>
            </div>
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>

    </modal>
</template>
<script>

    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    export default {
        components: {
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                loading: false,
                render: 0
            }
        },
        validations() {
            return {
                monthlyCost: {
                    id: {},
                    monthlyRent: { required },
                    monthlySaleries: {},
                    monthlyUtilityBills: {},
                    monthlyGovtFees: {},
                    monthlyGovtZakat: {},
                    govtFeeForLabour: {}
                }
            }
        },
        methods: {
            close: function () {
                this.$emit('close');
            },
            SaveMonthlyCost: function () {
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/Product/SaveMonthlyCost', this.monthlyCost, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.isSuccess == true) {
                        if (root.type != "Edit") {

                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: "Your Monthly Cost has been created!",
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 800,
                                timerProgressBar: true,
                            });

                            root.close();
                        }
                        else {


                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                                text: "Your Monthly Cost has been updated!",
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 800,
                                timerProgressBar: true,
                            });
                            root.close();
                        }
                    }
                    else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: "Your Monthly Cost Already Exist!",
                            type: 'error',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 800,
                            timerProgressBar: true,
                        });
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

            if (this.monthlyCost.id == '00000000-0000-0000-0000-000000000000' || this.monthlyCost.id == undefined || this.monthlyCost.id == '')
                this.GetAutoCodeGenerator();

        }
    }
</script>
