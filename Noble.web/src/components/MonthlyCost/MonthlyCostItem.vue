<template>
    <div >
        <div class="invoice-table mt-4">
            <table class="table m-0">
                <thead class="thead-light table-hover">
                    <tr class="text-capitalize text-center">

                        <th class="text-start" style="width:35%;">{{ $t('MonthlyCost.Description') }}</th>
                        <th class="text-center" style="width:20%;">{{ $t('MonthlyCost.MonthlyCost') }}</th>
                        <th class="text-center" style="width:20%;">{{ $t('MonthlyCost.YearlyCost') }}</th>
                        <th class="text-center" style="width:20%;">{{ $t('MonthlyCost.Daily') }}</th>
                        <th style="width:5%;" class="text-center">{{ $t('PurchaseBillItem.Action') }}</th>

                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(monthlyCost,index) in monthlyCostItems" v-bind:key="monthlyCost.id">


                        <td>
                            <textarea class="form-control borderNone" v-model="monthlyCost.description" v-bind:placeholder="$t('PurchaseBillItem.WriteHere')" />
                        </td>
                        <td>
                            <input class="form-control borderNone text-center" @update:modelValue="updateLineTotal($event.target.value, 'monthlyCosts', monthlyCost)" v-model="monthlyCost.monthlyCosts" type="number" @click="$event.target.select()" :text-dir="'true'" />
                        </td>
                        <td>
                            <input class="form-control borderNone text-center" v-model="monthlyCost.yearlyCost" @update:modelValue="updateLineTotal($event.target.value, 'yearlyCost', monthlyCost)" type="number" @click="$event.target.select()" :text-dir="'true'" />
                        </td>
                        <td>
                            <input class="form-control borderNone text-center" disabled v-model="monthlyCost.daily" type="number" @update:modelValue="updateLineTotal($event.target.value, 'daily', monthlyCost)" @click="$event.target.select()" :text-dir="'true'" />
                        </td>
                        <td style="width:60px;text-align:center" v-if="index == monthlyCostItems.length - 1 && addItem == false">
                            <button title="Add New Item" class="btn btn-primary btn-sm btn-round btn-icon float-right" v-on:click="addMonthlyCost">
                                <i class="fa fa-check"></i> {{ $t('MonthlyCost.AddNew') }}
                            </button>

                        </td>
                        <td style="width:60px;text-align:center" v-else>
                            <button title="Remove Item" class="btn r  btn-sm" v-on:click="removeExpense(monthlyCost.id, 'true')">
                                <i class="las la-trash-alt text-danger font-16"></i>
                            </button>
                        </td>
                    </tr>

                    <tr v-if="addItem">


                        <td>
                            <textarea class="form-control borderNone" v-model="monthlyCosts.description" v-bind:placeholder="$t('PurchaseBillItem.WriteHere')" />
                        </td>
                        <td>
                            <input class="form-control borderNone text-center" v-model="monthlyCosts.monthlyCosts" @update:modelValue="updateLineTotal($event.target.value, 'monthlyCosts', monthlyCosts)" type="number" @click="$event.target.select()" />
                        </td>
                        <td>
                            <input class="form-control borderNone text-center" v-model="monthlyCosts.yearlyCost" @update:modelValue="updateLineTotal($event.target.value, 'yearlyCost', monthlyCosts)" type="number" @click="$event.target.select()" />
                        </td>
                        <td>
                            <input class="form-control borderNone text-center" v-model="monthlyCosts.daily" disabled type="number" @update:modelValue="updateLineTotal($event.target.value, 'daily', monthlyCosts)" @click="$event.target.select()" />
                        </td>
                        <td class="text-center" v-if="(monthlyCosts.description=='' ) ">
                            <button title="Add New Item" class="btn btn-primary btn-sm btn-round btn-icon float-right " disabled v-on:click="addMonthlyCost">
                                <i class="fa fa-check"></i>
                            </button>
                        </td>

                        <td class="text-center" v-else-if="( monthlyCosts.monthlyCosts==0) ">
                            <button title="Add New Item" class="btn btn-primary btn-sm btn-round btn-icon float-right " disabled v-on:click="addMonthlyCost">
                                <i class="fa fa-check"></i>
                            </button>
                        </td>
                        <td class="text-center" v-else>
                            <button title="Add New Item" class="btn btn-primary btn-sm btn-round btn-icon float-right " v-on:click="addMonthlyCost">
                                <i class="fa fa-check"></i>
                            </button>
                        </td>
                    </tr>

                </tbody>

            </table>
            <div class="row" v-bind:key="refresh + 'g'">
                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-8 "></div>
                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-4">
                    <div>
                        <table class="table" style="background-color: #F1F5FA;">

                            <tbody>
                                <tr>
                                    <td colspan="2" class="pb-3 pt-3 fw-bold " style="width: 65%; font-weight: bolder; font-size: 14px;">
                                        {{ $t('MonthlyCost.TotalDaily') }}

                                    </td>

                                    <td class="text-end" style="width: 35%;">
                                        {{currency}}   {{parseFloat(summary.daily).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="pb-3 pt-3 fw-bold " style="width: 65%; font-weight: bolder; font-size: 14px;">
                                        {{ $t('MonthlyCost.TotalMonthlyCost') }}
                                    </td>

                                    <td class="text-end" style="width: 35%;">
                                        {{currency}} {{parseFloat(summary.monthlyCosts).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="pt-3 fw-bold " style="width: 65%; font-size: 14px;">
                                        {{ $t('MonthlyCost.TotalYearlyCost') }}
                                    </td>
                                    <td class="text-end" style="width: 35%;">
                                        {{currency}}   {{parseFloat(summary.yearlyCost).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                    </td>
                                </tr>


                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <!--<div class=" table-responsive mt-3"
                 v-bind:key="refresh + 'g'">
                <table class="table m-0">
                    <thead class="thead-light table-hover">
                        <tr class="text-right">
                            <th class="text-left">
                                #
                            </th>
                            <th class="text-center">{{ $t('MonthlyCost.TotalYearlyCost') }}</th>
                            <th class="text-center">{{ $t('MonthlyCost.TotalMonthlyCost') }}</th>
                            <th class="text-center">{{ $t('MonthlyCost.TotalDaily') }}</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr class="text-right samary_tbl_pd">
                            <td class="text-left">
                                {{ summary.item }}
                            </td>
                            <td class="text-center">
                                {{currency}}   {{parseFloat(summary.yearlyCost).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                            </td>
                            <td class="text-center">
                                {{currency}} {{parseFloat(summary.monthlyCosts).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                            </td>
                            <td class="text-center">
                                {{currency}}   {{parseFloat(summary.daily).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                            </td>


                        </tr>
                    </tbody>
                </table>
            </div>-->
        </div>
    </div>
</template>

<script>
    import moment from "moment";

    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        props: ['monthlyCost'],
        data: function () {
            return {
                monthlyCostItems: [],
                monthlyCosts: {
                    id: '',
                    description: '',
                    monthlyCosts: 0.00,
                    yearlyCost: 0.00,
                    daily: 0.00,
                    Total: 0.00,
                },
                loading: false,
                refresh: 0,
                currentItem: {
                    id: '',
                    description: '',
                    monthlyCosts: 0.00,
                    yearlyCost: 0.00,
                    daily: 0.00,
                    Total: 0.00,
                },
                summary: {
                    monthlyCosts: 0,
                    yearlyCost: 0,
                    daily: 0,
                    item: 0,
                },
                addItem: false,
                IsExpenseAccount: false,
                tableLength: 0,
                cardLength: 0,
                currency: '',
            }
        },
        validations() {
            
        },
        filters: {
            roundOffFilter: function (value) {

                return parseFloat(value).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
            }
        },


        methods: {
            calcuateSummary: function () {
                
                this.summary.item = this.monthlyCostItems.length;

                this.summary.monthlyCosts = this.monthlyCostItems
                    .reduce((total, prod) => total + parseFloat(prod.monthlyCosts), 0)
                    .toFixed(3).slice(0, -1);
                this.summary.yearlyCost = this.monthlyCostItems
                    .reduce((total, prod) => total + parseFloat(prod.yearlyCost), 0)
                    .toFixed(3).slice(0, -1);
                this.summary.daily = this.monthlyCostItems
                    .reduce((total, prod) => total + parseFloat(prod.daily), 0)
                    .toFixed(3).slice(0, -1);
               




            },
            updateLineTotal: function (e, prop, monthlyCosts) {


                
                
                if (prop == "yearlyCost") {
                    if (e < 0 || e == '' || e == undefined) {
                        e = '';
                    }
                    else {
                        monthlyCosts.yearlyCost = e;
                    }
                }
                if (prop == "monthlyCosts") {
                    if (e < 0 || e == '' || e == undefined) {
                        e = '';
                    }
                    else {
                        monthlyCosts.amount = e;
                    }
                }
                if (prop == "daily") {
                    if (e < 0 || e == '' || e == undefined) {
                        e = '';
                    }
                    else {
                        monthlyCosts.daily = e;
                    }
                }
                //var monthDays = moment().daysInMonth();
                
                var yearDays;
                if (moment().isLeapYear()) {
                    yearDays = 366
                }
                else {
                    yearDays = 365
                }
                console.log(yearDays);

                if (prop == "yearlyCost") {
                    monthlyCosts.monthlyCosts = ((monthlyCosts.yearlyCost / 12)).toFixed(2);
                    monthlyCosts.daily = ((monthlyCosts.yearlyCost / yearDays)).toFixed(2);
                }
                if (prop == "monthlyCosts") {
                    monthlyCosts.yearlyCost = ((monthlyCosts.monthlyCosts * 12)).toFixed(2);
                    monthlyCosts.daily = ((monthlyCosts.monthlyCosts / 30.41)).toFixed(2);
                }
                if (prop == "daily") {
                    monthlyCosts.yearlyCost = ((monthlyCosts.daily / yearDays)).toFixed(2);
                    monthlyCosts.monthlyCosts = ((monthlyCosts.daily / 12)).toFixed(2);
                }
            



                  
                 
                        

                   
              
                this.monthlyCostItems['monthlyCosts'] = monthlyCosts



                this.calcuateSummary();

                
                this.$emit('update:modelValue', this.monthlyCostItems);

            },

            checkTableWidth: function () {
                if (document.getElementsByClassName('itemtable')[0] != undefined) {
                    this.tableLength = document.getElementsByClassName('itemtable')[0].clientWidth;
                    this.cardLength = document.getElementsByClassName('card')[0].clientWidth - 25;
                }
            },
            
            addMonthlyCost: function () {

                this.loading = true;
                this.monthlyCostItems.push({
                    id: this.createUUID(),
                    daily: this.monthlyCosts.daily,
                    yearlyCost: this.monthlyCosts.yearlyCost,
                    monthlyCosts: this.monthlyCosts.monthlyCosts,
                    description: this.monthlyCosts.description
                });

                this.monthlyCosts = {
                    id: '',
                    daily: 0.00,
                    yearlyCost: 0.00,
                    monthlyCosts: 0.00,
                    description: '',
                };
                this.calcuateSummary();

                this.refresh += 1;
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

            removeExpense: function (id) {

                var ds = this.monthlyCostItems.findIndex(function (i) {
                    return i.id === id;
                });

                this.monthlyCostItems.splice(ds, 1);
            },
        },
        created: function () {

           
        },
        updated: function () {

            document.querySelector("html").classList.remove("perfect-scrollbar-on");
            this.$emit('updatedailyExpenseRows', this.monthlyCostItems);
        },
        mounted: function () {
            
            if (this.monthlyCost != undefined && this.monthlyCost != '' && this.monthlyCost !=null ) {


                this.monthlyCostItems = this.monthlyCost.monthlyCostItems;
                this.calcuateSummary();
            }
            this.currency = localStorage.getItem('currency');


            this.addItem = this.monthlyCostItems.length > 0 ? false : true;
        }
    }</script>