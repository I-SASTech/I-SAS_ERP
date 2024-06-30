<template>
    <div>
        <div >
            <div class=" mt-4">
                <table class="table mb-0">
                    <thead class="thead-light table-hover">
                        <tr class="text-capitalize text-center">
                            <th class="text-start" style="width: 5%">
                                #
                            </th>
                            <th class="text-start" style="width: 60%;">
                                {{ $t('AddTemporaryCashRequest.Description') }}
                            </th>

                            <th style="width: 35%;" class="text-center">
                                {{ $t('AddTemporaryCashRequest.Amount') }}
                            </th>
                            <th style="width: 10%"></th>
                        </tr>
                    </thead>
    
                    <tbody id="purchase-item">
                            <tr v-for="(prod , index) in temporaryCash" :key="index">
                                <td class="border-top-0">
                                    {{index+1}}
                                </td>
                                <td>
                                    <textarea rows="2" class="form-control" v-model="prod.description" />
                                </td>

                                <td>
                                    <decimal-to-fixed v-model="prod.amount" />
                                </td>
                                <td >
                                    <button @click="removeProduct(prod.rowId)"
                                            title="Remove Item"
                                            class="btn r  btn-sm">
                                        <i class="las la-trash-alt text-danger font-16"></i>
                                    </button>
                                </td>

                            </tr>
                        <tr>
                            <td >
                            </td>
                            <td >
                                <textarea rows="2" class="form-control" v-model="description"  />
                            </td>

                            <td>
                                <decimal-to-fixed v-model="amount" />
                            </td>
                            <td class="text-center">
                             
                                <button @click="addProduct()"
                                        v-bind:disabled="amount<=0 || description==''"
                                        title="Add Item"
                                        class="btn btn-primary btn-sm btn-round btn-icon float-right ">
                                    <i class="fa fa-check"></i>
                                </button>
                            </td>
                        </tr>



                    </tbody>

                </table>
                <div class="row" v-bind:key="rendered + 'g'">
                    <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 "></div>
                    <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                        <div>
                            <table class="table" style="background-color: #F1F5FA;">
                               
                                <tbody>

                                    <tr>
                                        <td colspan="2" class="pt-3 fw-bold " style="width: 65%;">
                                            {{ $t('AddTemporaryCashRequest.NoItem') }}
                                        </td>
                                        <td class="text-end" style="width: 35%;">
                                            {{ summary.item }}
                                        </td>
                                        </tr>
                                    <tr>
                                        <td colspan="2" class="pb-3 pt-3 fw-bold " style="width: 65%; font-weight: bolder; font-size: 16px;">
                                            {{ $t('AddTemporaryCashRequest.TotalAmount') }}
                                        </td>

                                        <td class="text-end" style="width: 35%;">
                                            {{ summary.amount}}
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>

            </div>

        </div>

        


    </div>
</template>

<script>
    export default {
        name: "TemporaryCash",

        data: function () {
            return {
                rendered: 0,
                temporaryCash: [],
                loading: false,
                summary: {
                    item: 0,
                    amount: 0,
                },
                currency: '',
                amount: 0,
                description: '',
            };
        },
        methods: {            
            calcuateSummary: function () {
                this.summary.item = this.temporaryCash.length;
                this.summary.amount = this.temporaryCash.reduce((totalQty, prod) => totalQty + parseFloat(prod.amount), 0);

                this.$emit("update:modelValue", this.temporaryCash);
            },


            addProduct: function () {
                var root = this;        
                this.temporaryCash.push({
                    rowId: root.createUUID(),
                    description: root.description,
                    amount: root.amount
                });

                this.description = '';
                this.amount = 0;
                this.calcuateSummary();
            },


            createUUID: function () {
                var dt = new Date().getTime();
                var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
                    var r = (dt + Math.random() * 16) % 16 | 0;
                    dt = Math.floor(dt / 16);
                    return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
                });
                return uuid;
            },
            removeProduct: function (id) {

                this.temporaryCash = this.temporaryCash.filter((prod) => {
                    return prod.rowId != id;
                });

                this.calcuateSummary();
            },
        },
        created: function () {
            var root = this;

            if (this.$route.query.data != undefined) {

                root.$route.query.data.temporaryCashItems.forEach(function (item) {
                    root.temporaryCash.push({
                        rowId: item.id,
                        description: item.description,
                        amount: item.amount
                    });
                });
                this.calcuateSummary();
            }
        },
        mounted: function () {
            this.currency = localStorage.getItem('currency');
        },
    };
</script>

<style scoped>
    #sale-item td {
        padding-bottom: 0px;
        padding-top: 0px;
    }

    .input-border {
        border: transparent;
        background-color: transparent !important;
    }

        .input-border:focus {
            outline: none !important;
            border: none !important;
        }

    .multiselect__tags {
        background-color: transparent !important;
    }

    .multiselect__input, .multiselect__single {
        background-color: transparent !important;
    }

    .tableHoverOn {
        background-color: #ffffff !important;
        height: 32px !important;
        max-height: 32px !important;
    }
</style>
