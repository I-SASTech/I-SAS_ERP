<template>
    <modal :show="show" :modalLarge="true">

        <div style="margin-bottom:0px" class="card" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="modal-header">

                            <h5 class="modal-title DayHeading" id="myModalLabel" v-if="approvalStatus.action=='Rejected'">{{ $t('RejectedExpenseList') }} </h5>
                            <h5 class="modal-title DayHeading" id="myModalLabel" v-else> {{ $t('ActionModel.ApproveExpenseList') }} </h5>

                        </div>

                        <div class="">
                            <div class="card-body">
                                <div class="row ">
                                    <div class="col-lg-6 col-md-6 col-sm-6" v-if="approvalStatus.action=='Rejected'">
                                        <h6 class="label">{{ $t('ActionModel.PleaseSelectYourPaymentType') }}</h6>
                                        <div class="form-check form-check-inline">

                                            <input class="form-check-input" v-model="approvalStatus.paymentType" type="radio" name="inlineRadioOptions" id="inlineRadio1" value="Cash">
                                            <label class="form-check-label" for="inlineRadio1">{{ $t('ActionModel.Cash') }}</label>
                                        </div>
                                        <div class="form-check form-check-inline">
                                            <input class="form-check-input" type="radio" v-model="approvalStatus.paymentType" name="inlineRadioOptions" id="inlineRadio2" value="Credit">
                                            <label class="form-check-label" for="inlineRadio2">{{ $t('ActionModel.Credit') }}</label>
                                        </div>

                                    </div>
                                    <div style="height: 300px; overflow: auto; ">
                                        <table class="table table-shopping" style="table-layout:fixed;width:100%;" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                            <thead class="">
                                                <tr>
                                                    <th>
                                                        #
                                                    </th>
                                                    <th>
                                                        {{ $t('ActionModel.VoucherNo') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('ActionModel.Date') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('ActionModel.Description') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('ActionModel.TotalAmount') }}
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(details,index) in approvalStatus.selected" v-bind:key="details.id">
                                                    <td>
                                                        {{index+1}}
                                                    </td>
                                                    <td>
                                                        {{details.voucherNo}}
                                                    </td>
                                                    <td>
                                                        {{details.date}}
                                                    </td>
                                                    <td>
                                                        {{details.description}}
                                                    </td>
                                                    <td>
                                                        {{currency}}  {{parseFloat(details.totalAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                    </td>

                                                </tr>
                                            </tbody>

                                        </table>

                                    </div>
                                    <div style="width:100%;text-align:right;padding-right:10px;padding-top:20px"><label>Total:</label> {{$formatAmount(approvalStatus.total)  }}</div>
                                    <div class="col-lg-12 col-md-12 col-sm-12" v-if="approvalStatus.action=='Rejected'">
                                        <label>{{ $t('ActionModel.Reason') }} :<span class="text-danger"> *</span></label>
                                        <div>
                                            <textarea class="form-control" v-model="approvalStatus.reason"></textarea>
                                        </div>
                                    </div>


                                    <div class="col-lg-6 col-md-6 col-sm-6" v-if="approvalStatus.action=='Approved'">
                                        <label>{{ $t('ActionModel.PleaseSelectAccount') }} :</label>
                                        <div>
                                            <accountdropdown v-model="approvalStatus.accountId" :formName="approvalStatus.accountSelect"></accountdropdown>
                                        </div>
                                    </div>



                                </div>
                            </div>
                        </div>

                        <div class="modal-footer justify-content-right">

                            <button type="button" class="btn btn-primary  " v-on:click="SaveCity" v-bind:disabled="v$.approvalStatus.$invalid"> {{ $t('ActionModel.btnSave') }}</button>
                            <button type="button" class="btn btn-danger  mr-3 " v-on:click="close(false)">{{ $t('ActionModel.btnClear') }}</button>

                        </div>
                    </div>
                </div>
            </div>
        </div>

    </modal>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    
    import { requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'

    export default {
        props: ['show', 'approvalStatusVm'],
        mixins: [clickMixin],
        setup() {
            return { v$: useVuelidate() }
        },

        data: function () {
            return {
                approvalStatus: this.approvalStatusVm,
                render: 0,
                currency: '',
                selectedId: []
            }
        },
        validations() {
          return{
              approvalStatus: {
                accountId: {
                     required: requiredIf(function () {
                            return this.approvalStatus.action == 'Approved' ? true : false;
                        }),
                    // required: requiredIf((x) => {
                    //     if (x.action == 'Approved')
                    //         return true;
                    //     return false;
                    // }),
                },
                reason: {
                    required: requiredIf(function () {
                            return this.approvalStatus.action == 'Rejected' ? true : false;
                        }),
                    // required: requiredIf((x) => {
                    //     if (x.action == 'Rejected')
                    //         return true;
                    //     return false;
                    // }),
                },
                paymentType: {
                    required: requiredIf(function () {
                            return this.approvalStatus.action == 'Rejected' ? true : false;
                        }),
                    // required: requiredIf((x) => {
                    //     if (x.action == 'Rejected')
                    //         return true;
                    //     return false;
                    // }),
                },

            }
          }
        },
        methods: {
            close: function (x) {

                if (x) {
                    this.$emit('close', this.approvalStatus.action);

                }
                else {
                    this.$emit('close', true);
                }
            },
            SaveCity: function () {

                var root = this;
                for (var i in this.approvalStatus.selected) {

                    this.selectedId[i] = this.approvalStatus.selected[i].id
                }
                //root.approvalStatus.selected.forEach(function (x) {
                //    root.selectedId.push({
                //        selectedId: x.id,
                //    });

                //})

                this.approvalStatus.selectedId = this.selectedId;
                this.selectedId = [];

                var url = '/Company/UpdateApprovalStatus';
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https
                    .post(url, root.approvalStatus, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        if (response.data != null) {
                            this.$swal.fire({
                                title: root.$t('ActionModel.SavedSuccessfully'),
                                text: root.$t('ActionModel.Saved'),
                                type: 'success',
                                confirmButtonClass: "btn btn-success",
                                buttonStyling: false,
                                icon: 'success',
                                timer: 1500,
                                timerProgressBar: true,

                            });
                            root.close(true);
                        }
                    })
                    .catch(error => {

                        console.log(error)
                        this.$swal.fire(
                            {
                                icon: 'error',
                                title: this.$t('ActionModel.Error'),
                                text: this.$t('ActionModel.Error'),
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
