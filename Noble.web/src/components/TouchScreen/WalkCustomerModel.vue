<template>
    <modal :show="show">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel"> {{ $t('WalkCustomerModel.Customer') }}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row ">
                    <div class="form-group  col-sm-12 ">
                        <label class="text  font-weight-bolder"> {{ $t('WalkCustomerModel.Customer') }}: </label>
                        <customerdropdown v-model="sale.customerId" ref="CustomerDropdown" @update:modelValue="emptyCashCustomer" :isCredit="false" :modelValue="sale.customerId"></customerdropdown>
                    </div>
                    <!-- <div class="form-group  col-sm-12 ">
                        <label class="text  font-weight-bolder"> {{ $t('WalkCustomerModel.Search') }}:</label>
                        <input class="form-control" @update:modelValue="cashCustomerSearch(search)" v-model="search"  type="text" />
                    </div>

                    <div class="form-group  col-sm-12 ">
                        <label class="text  font-weight-bolder"> {{ $t('WalkCustomerModel.Customer') }}:</label>
                        <input class="form-control" v-bind:disabled="walkIn" v-model="sale.cashCustomer"  type="text" />

                    </div>
                    <div class="form-group  col-sm-6 ">
                        <label class="text  font-weight-bolder"> {{ $t('WalkCustomerModel.CustomerId') }}:</label>
                        <input class="form-control" v-model="sale.cashCustomerId"  type="text" />
                    </div>
                    <div class="form-group  col-sm-6 ">
                        <label class="text  font-weight-bolder"> {{ $t('WalkCustomerModel.Mobile') }}:</label>
                        <input class="form-control" v-bind:disabled="walkIn" v-model="sale.mobile"  type="text" />

                    </div>
                    <div class="form-group  col-sm-12 ">
                        <label class="text  font-weight-bolder"> {{ $t('WalkCustomerModel.RegisterUser_EmailID') }}:</label>
                        <input class="form-control" v-bind:disabled="walkIn" v-model="sale.email"  type="text" />
                    </div> -->

                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveOrigin">{{ $t('WalkCustomerModel.Select') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('WalkCustomerModel.Close') }}</button>
            </div>
        </div>
    </modal>

</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'

    export default {
        mixins: [clickMixin],
        props: ['show', 'walkcustomer'],
        data: function () {
            return {
                search: '',
                walkIn: false,
                save: false,
                customerDetail: {
                    id: "00000000-0000-0000-0000-000000000000",
                    customerCode: "",
                    advanceAccountId: "",
                    commercialRegistrationNo: "",
                    customerIdForUpdate: "",
                    shippingAddress: "",
                    address: "",
                    contactNo1: "",
                    vatNo: "",                    
                },
                sale: {},
            }
        },
        methods: {
            emptyCashCustomer: function (customerId, advanceAccountId, customerDetail) {


                if (customerDetail.englishName == null) {
                    customerDetail.englishName = ''
                }
                if (customerDetail.arabicName == null) {
                    customerDetail.arabicName = ''
                }

                this.sale.cashCustomer = customerDetail.englishName + ' ' + customerDetail.arabicName


            },
            // cashCustomerSearch: function (value) {
            //     var root = this;
            //     var token = '';
            //     if (token == '') {
            //         token = localStorage.getItem('token');
            //     }

            //     if (value!='') {
            //         this.$https.get('/Sale/SearchCashCustomer?search=' + value, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
            //             if (response.data != null) {
            //                 root.sale.cashCustomer = response.data.name;
            //                 root.sale.mobile = response.data.mobile;
            //                 root.sale.email = response.data.email;
            //                 root.sale.cashCustomerId = response.data.customerId;
            //             }
            //         })
            //     }
            //     else {
            //         root.sale.cashCustomer = 'Walk-In';
            //         root.sale.mobile = '';
            //         root.sale.cashCustomerId = '';
            //         root.sale.email = '';
            //     }

            // },
            close: function () {
                if (this.save) {
                    this.$emit('close', this.sale);
                }
                else {
                    this.$emit('close', false);

                }
            },
            DisableWalkIn: function (x) {

                if (x != null || x != undefined) {
                    this.walkIn = true;
                    this.sale.mobile = '';
                    this.sale.email = '';
                    this.sale.cashCustomer = '';
                    this.sale.cashCustomerId = '';
                    this.search = '';
                }
                else {
                    this.sale.cashCustomer = 'Walk-In';
                    this.walkIn = false;
                }
            },
            SaveOrigin: function () {
                // if (this.sale.cashCustomer === '') {
                //     this.sale.cashCustomer = 'Walk-In';
                // }
                this.save = true;
                var root = this;

                root.close();
            }
        },

        created: function () {
            this.sale = this.walkcustomer;
        },

        mounted: function () {
            this.saleTemp = this.sale;

            if (this.sale.customerId) {
                this.walkIn = true;
            }
        }
    }
</script>
