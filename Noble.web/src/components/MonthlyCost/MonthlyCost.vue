<template>
    <div class="row" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-if="isValid('CanAddMonthlyCost')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{  $t('MonthlyCost.AddMonthlyCost') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{$t('PurchaseOrder.Home')}}</a></li>
                                    <li class="breadcrumb-item active">{{  $t('MonthlyCost.AddMonthlyCost') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                                    {{ $t('PurchaseOrder.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-header">
                    <h5>{{ $t('MonthlyCost.AddMonthlyCost') }}</h5>
                </div>
                <div class="card-body">

                    <div class="row ">


                        <div class="form-group has-label col-12 ">
                            <monthlycostitem :monthlyCost="monthly" @update:modelValue="getupdatedailyExpenseRows" v-bind:key="renderCost"></monthlycostitem>
                        </div>



                    </div>


                </div>

            </div>

        </div>
        <div class="col-lg-12 invoice-btn-fixed-bottom">
            <div class="button-items">
                <button type="button" class="btn btn-outline-primary  " v-on:click="SaveMonthlyCost" v-bind:disabled="monthlyCost.$invalid"><i class="far fa-save"></i>  {{ $t('AddMonthlyCost.btnSave') }}</button>
                <button type="button" class="btn btn-danger  " v-on:click="close()">{{ $t('AddMonthlyCost.btnClear') }}</button>
            </div>
        </div>
    </div>
    <div v-else> <acessdenied></acessdenied></div>

</template>

<script>
    import clickMixin from '@/Mixins/clickMixin';
    export default {

        mixins: [clickMixin],
        data: function () {
            return {

                monthlyCostlist: [],
                renderCost: 0,
                monthly:null,
                monthlyCost: {
                    id: '00000000-0000-0000-0000-000000000000',
                    date: '',
                    branchId: '',
                    
                    monthlyCostItems:[]

                }




            }
        },


        methods: {
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            getupdatedailyExpenseRows: function (monthlyCostItems) {
                
                this.monthlyCost.monthlyCostItems = monthlyCostItems;
            },
            close: function () {
                this.$router.push('/StartSCreen')
            },
            SaveMonthlyCost: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                this.$https.post('/Company/SaveMonthlyCost', this.monthlyCost, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data.isSuccess == true) {
                         {

                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: "Your Monthly Cost has been Created!",
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 800,
                                timerProgressBar: true,
                            });


                        }
                        
                    }
                    else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: "Your Monthly Cost Already Exist!",
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 1200,
                            timerProgressBar: true,
                        });
                    }
                });
            },
            EditMonthlyCost: function () {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Company/MonthlyCostDetail', { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        
                        if ( response.data != "" ) {
                            
                            root.monthlyCost = response.data;
                            root.monthly = response.data;
                            root.renderCost++;

                        } else {
                            console.log("error: something wrong from db.");
                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });

            }
        },
        created: function () {
            this.EditMonthlyCost();

            this.$emit('update:modelValue', this.$route.name);
        },
        mounted: function () {
            this.monthlyCost.branchId = localStorage.getItem('BranchId');
        }
    }
</script>