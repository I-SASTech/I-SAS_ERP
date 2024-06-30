<template>
    <div v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" style="z-index:9999 !important">
        <modal :show="show" :modalLarge="true">
            <div class="modal-content">
                <div class="col-lg-12">
                    <div class="modal-header">
                        <div class="col-sm-12">
                            <div class="page-title-box">
                                <div class="row">
                                    <div class="col">
                                        <h4 class="modal-title m-0" id="exampleModalDefaultLabel"> Delivery Note </h4>

                                    </div>


                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="modal-body">
                        <div class="col-md-12">
                            <DeliveryChallanItem @update:modelValue="SavePurchaseItems" :deliveryChallanItems="purchase.saleItems"
                                                 :isReservedChallan="isReservedChallan" :key="rander" :isTemplate="true"
                                                 :isService="isService" />
                        </div>
                        <loading v-model:active="loading" :can-cancel="false" :is-full-page="false"></loading>
                        <div class="col-lg-12 ">
                        </div>

                    </div>
                </div>
                <div class="modal-footer">
                    <div>
                        <div class="button-items">
                            <button class="btn btn-primary  mr-2" v-if="type"
                                    v-on:click="savePurchase('Approved')"
                                    >

                                <i class="far fa-save"></i>  Save
                            </button>
                            <button class="btn btn-primary  mr-2" v-else
                                    v-on:click="savePurchase('Approved')"
                                    >

                                <i class="far fa-save"></i>  Update
                            </button>
                            <button class="btn btn-danger  mr-2"
                                    v-on:click="goToPurchase">
                                {{ $t('AddQuotation.Cancel') }}
                            </button>
                        </div>
                    </div>

                </div>


            </div>


        </modal>
    </div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
  import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
     //import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'


    export default {
        mixins: [clickMixin],
        props: ['newshow', 'newpurchase', 'type', 'isReservedChallan', 'isSaleOrder', 'isService', 'deliveryUndefined'],

          components: {
            Loading
        },
        //setup() {
        //    return { v$: useVuelidate() }
        //},

        data: function () {
            return {
                show: this.newshow,
                purchase: this.newpurchase,
                randerCustomer: 0,
                daterander: 0,
                rander: 0,
                render: 0,
                loading: false,

                itemRender: 0,
                serviceId: '',

            };
        },


        //validations() {
        //    return {
        //        purchase: {
        //            date: {},
        //            description: {},
        //            refrence: {},


        //            deliveryChallanItems: {},
        //        },
        //    }
        //},
        methods: {

            createUUID: function () {
                var dt = new Date().getTime();
                var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
                    var r = (dt + Math.random() * 16) % 16 | 0;
                    dt = Math.floor(dt / 16);
                    return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
                });
                return uuid;
            },

            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            Attachment: function () {
                this.show = true;
            },

            attachmentSave: function (attachment) {
                this.purchase.attachmentList = attachment;
                this.show = false;
            },


            RanderCustomer: function () {
                this.randerCustomer++;
            },

            AutoIncrementCode: function () {

                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                var service = false
                if (this.isService) {
                    service = true;
                }
                root.$https
                    .get('/Purchase/DeliveryChallanAutoGenerateNo?IsService=' + service, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.purchase.registrationNo = response.data;
                        }
                    });
            },
            SavePurchaseItems: function (deliveryChallanItems) {

                this.purchase.deliveryChallanItems = deliveryChallanItems;
            },
            savePurchase: function (status) {


                this.purchase.isReserved = true;

                this.purchase.approvalStatus = status;

                if (this.type == true) {
                    if (this.isSaleOrder == true) {
                        this.purchase.saleOrderId = this.purchase.id;
                        this.purchase.id = "00000000-0000-0000-0000-000000000000";
                    }
                    else {
                        this.purchase.saleInvoiceId = this.purchase.id;
                        this.purchase.id = "00000000-0000-0000-0000-000000000000";
                    }


                }
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }


                this.$https
                    .post('/Purchase/SaveDeliveryChallanInformation', root.purchase, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        root.loading = false
                        root.goToPurchase();
                        root.info = response.data

                        root.$swal({
                            title: "Saved!",
                            text: "Data Saved Successfully!",
                            type: 'success',
                            icon: 'success',
                            timer: 1500,
                            timerProgressBar: true,
                        }).then(function (response) {
                            if (response != undefined) {

                                root.goToPurchase();
                            }
                        });

                    })
                    .catch(error => {
                        console.log(error)
                        if (localStorage.getItem('IsMultiUnit') == 'true') {
                            root.purchase.deliveryChallanItems.forEach(function (x) {

                                x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.unitPerPack));
                                x.quantity = parseInt(parseInt(x.quantity) % parseInt(x.unitPerPack));

                            });
                        }
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: 'Something Went Wrong!',
                                text: error,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false)

            },

            goToPurchase: function () {

                this.$emit('close');





            },
        },
        created: function () {

            var aa = '';
            if (this.deliveryUndefined == true) {
                aa = undefined;
            }

            if (this.isSaleOrder == true) {
                if (this.purchase.saleOrderItems == aa) {
                    this.purchase.saleItems = this.purchase.deliveryChallanItems
                }
                else {
                    this.purchase.saleItems = this.purchase.saleOrderItems;
                    this.purchase.saleItems.forEach(function (result) {
                        result.isActive = true;
                    })
                }
            }
            else {
                if (this.purchase.saleItems == aa) {
                    this.purchase.saleItems = this.purchase.deliveryChallanItems
                }
                else {
                    this.purchase.saleItems.forEach(function (result) {
                        result.isActive = true;
                    })
                }
            }
        },
        mounted: function () {


        },
    };
</script>
