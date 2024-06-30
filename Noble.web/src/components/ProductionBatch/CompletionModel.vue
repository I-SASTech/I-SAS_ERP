<template>
    <modal :show="show" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">

        <div style="margin-bottom:0px" class="card">
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">

                        <div class="modal-header">

                            <h5 class="modal-title DayHeading" id="myModalLabel">  {{ $t('CompletionModel.BatchCompletion') }}</h5>

                        </div>

                        <div class="card-body">
                            <div class="row ">


                                <div class="form-group has-label col-sm-12 ">
                                    <span style="font-size: 18px; font-weight: 400">{{ $t('CompletionModel.AssumptionStock') }} : </span><span style="font-size:18px;font-weight: 400">   {{purchase.netAmount}}</span>

                                </div>

                                <div class="form-group has-label col-sm-12 ">
                                    <label class="text  font-weight-bolder">{{ $t('CompletionModel.RemainingStock') }}: <span class="text-danger"> *</span></label>
                                    <input class="form-control" type="number" v-model="purchase.remainingStock" @update:modelValues="zeroPrice(purchase.remainingStock)" @focus="$event.target.select()" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" />

                                </div>
                                <div class="form-group has-label col-sm-12 ">
                                    <span style="font-size: 18px; font-weight: 400">{{ $t('CompletionModel.DamageStock') }}: </span><span style="font-size:18px;font-weight: 400">   {{purchase.damageStock}}</span>

                                    <!--<label class="text  font-weight-bolder"> Damage Stock:{{purchase.damageStock}}</label>-->

                                </div>
                                <div class="form-group has-label col-sm-12 " v-if="reason">
                                    <label class="text  font-weight-bolder"> {{ $t('CompletionModel.ReasonForLate') }} :</label>
                                    <textarea class="form-control" v-model="purchase.lateReasonCompletion" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" type="text" />

                                </div>
                            </div>
                        </div>


                        <div class="modal-footer justify-content-right" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'direction: rtl'" v-if="!loading">

                            <button type="button" class="btn btn-primary  " v-on:click="SaveOrigin" v-if="disable" disabled>  {{ $t('CompletionModel.btnSave') }}</button>
                            <button type="button" class="btn btn-primary  " v-on:click="SaveOrigin" v-else v-bind:disabled="v$.purchase.$invalid">  {{ $t('CompletionModel.btnSave') }}</button>
                            <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()"> {{ $t('CompletionModel.btnClear') }}</button>

                        </div>
                        <div class="card-footer col-md-3" v-else>
                            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </modal>

</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

    import moment from "moment";
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'

    export default {
        components: {
            Loading
        },
        mixins: [clickMixin],
        props: ['show', 'newPurchase'],
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                purchase: this.newPurchase,
                loading: false,
                reason: false,
                UserName: '',
                disable: false

            }
        },
        validations() {
            return {
                purchase: {
                    remainingStock: {

                        required
                    },
                }
            }
        },

        methods: {
            zeroPrice: function (x) {

                if (x == 0) {
                    this.disable = true;
                    
                    //this.$swal({
                    //    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                    //    text: "Remaining Stock not be Zero!",
                    //    type: 'error',
                    //    icon: 'error',
                    //    showConfirmButton: false,
                    //    timer: 2000,
                    //    timerProgressBar: true,
                    //});
                }
                else {
                    this.disable = false;
                }
                this.DamageStock(x);
            },
            DamageStock: function (x) {
                this.purchase.damageStock = this.purchase.netAmount - x;
            },
            close: function () {



                this.$emit('close', false);


            },

            SaveOrigin: function () {

                this.purchase.approvalStatus = 'Complete';
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/Batch/BatchStatus', this.purchase, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data) {
                        root.loading = false;
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
                        root.$emit('RefreshList', 'Complete');
                    }

                    else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'There is some Error On Status Change!' : 'هناك بعض الخطأ في تغيير الحالة!',
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });
                    }
                });
            }
        },
        mounted: function () {
            this.UserName = localStorage.getItem('UserName');
            this.purchase.remainingStock = '';
            this.purchase.completeBy = this.UserName;


            if (this.purchase.endTime != null) {
                //var systemTime = moment().format('DD/MM/YYYY HH:mm:ss');
                var systemTime = moment().add(1, 'hour').format('YYYY-MM-DD HH');

                //
                //var startTime = moment(this.purchase.startTime).add(1, 'hour').format('YYYY-MM-DD HH');



                var endTime = moment(this.purchase.endTime).add(1, 'hour').format('YYYY-MM-DD HH');


                var years = moment(systemTime).isSame(endTime, 'year');
                if (years) {
                    var month = moment(systemTime).isSame(endTime, 'month ');
                    if (month) {
                        var days = moment(systemTime).isSame(endTime, 'day');
                        if (days) {
                            var hour = moment(systemTime).isSame(endTime, 'hour');

                            if (hour) {
                                this.reason = false;
                            }
                            else {
                                this.reason = true;
                            }

                        }
                        else {
                            this.reason = true;
                        }
                        //if (moment(startTime).isAfter(endTime)) {
                        //    var systemTimeBefore = moment().format('YYYY-MM-DD HH');
                        //    if (moment(systemTimeBefore).isBefore(endTime)) {
                        //        alert("Wao!You Complete befor End Time");
                        //        this.reason = false;

                        //    }
                        //    else {

                        //    }
                        //}


                    }
                    else {
                        this.reason = true;
                    }
                }
                else {
                    this.reason = true;
                }
            }
            else {
                this.reason = false;

            }

            //var diff = systemTime.diff(endTime);
            //var diff = endTime.diff(systemTime, 'minutes');
            //var ms = moment(endTime, "DD/MM/YYYY HH:mm").diff(moment(systemTime, "DD/MM/YYYY HH:mm"));
            //console.log(ms);

            //if (ms < 0 || ms > 900000 ) {
            //    this.reason = true;
            //}
            //else {
            //    this.reason = false;
            //}



            //var diff = moment(systemTime, "DD/MM/YYYY HH:mm:ss").diff(endTime, "DD/MM/YYYY HH:mm:ss").format("HH:mm:ss")
            //if (moment.utc(moment(systemTime, "DD/MM/YYYY HH:mm:ss") > moment.utc(moment(endTime, "DD/MM/YYYY HH:mm:ss")))) {
            //    alert(diff);

            //}
            //else {
            //    alert('false');
            //}
        }
    }
</script>
