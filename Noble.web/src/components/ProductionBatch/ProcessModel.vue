<template>
    <modal :show="show" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">

        <div style="margin-bottom:0px" class="card">
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">

                        <div class="modal-header">

                            <h5 class="modal-title DayHeading" id="myModalLabel">{{ $t('ProcessModel.InProcessBatch') }}</h5>

                        </div>

                        <div class="card-body">
                            <div class="row ">

                                <!--<div class="form-group has-label col-sm-12 ">
                                    <label class="text  font-weight-bolder"> Remaining Stock:</label>
                                    <input class="form-control" v-model="purchase.remainingStock" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" type="text" />

                                </div>
                                <div class="form-group has-label col-sm-12 ">
                                    <label class="text  font-weight-bolder"> Damage Stock:</label>
                                    <input class="form-control" v-model="purchase.damageStock" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" type="text" />

                                </div>-->
                                <div class="form-group has-label col-sm-12 ">
                                    <label class="text  font-weight-bolder">{{ $t('ProcessModel.WhyareyoulateProcessBatch') }} :</label>
                                    <textarea class="form-control" v-model="purchase.lateReason" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" type="text" />

                                </div>
                            </div>
                        </div>


                        <div class="modal-footer justify-content-right" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'direction: rtl'">

                            <button type="button" class="btn btn-primary  " v-on:click="SaveOrigin">  {{ $t('ProcessModel.btnSave') }}</button>
                            <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()"> {{ $t('ProcessModel.btnClear') }}</button>

                        </div>
                    </div>
                </div>
            </div>
        </div>

    </modal>

</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    
    export default {
        mixins: [clickMixin],
        props: ['show', 'newPurchase'],
        data: function () {
            return {
                purchase: this.newPurchase,
                reason: false

            }
        },
        methods: {
            close: function () {



                this.$emit('close', false);


            },

            SaveOrigin: function () {

                this.purchase.approvalStatus = 'InProcess';
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/Batch/BatchStatus', this.purchase, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    
                    if (response.data) {
                        

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
                        root.$emit('RefreshList', 'InProcess');
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

            
            //var systemTime = moment().format('DD/MM/YYYY HH:mm:ss');
            //var startTime = moment(this.purchase.startTime).add(15, 'minutes').format('DD/MM/YYYY HH:mm:ss');
            ////var diff = systemTime.diff(startTime);
            ////var diff = startTime.diff(systemTime, 'minutes');
            //if (systemTime > startTime) {
            //    this.reason = true;
            //}
            //else {
            //    this.reason = false;
            //}



            //var diff = moment(systemTime, "DD/MM/YYYY HH:mm:ss").diff(startTime, "DD/MM/YYYY HH:mm:ss").format("HH:mm:ss")
            //if (moment.utc(moment(systemTime, "DD/MM/YYYY HH:mm:ss") > moment.utc(moment(startTime, "DD/MM/YYYY HH:mm:ss")))) {
            //    alert(diff);

            //}
            //else {
            //    alert('false');
            //}
        }
    }
</script>
