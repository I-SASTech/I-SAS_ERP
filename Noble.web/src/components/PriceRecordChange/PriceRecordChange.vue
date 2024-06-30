<template>
    <div class="row">

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('PriceRecordChange.PriceRecordChange') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('PriceRecord.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('PriceRecordChange.PriceRecordChange') }}</li>
                                </ol>
                            </div>
                          
                        </div>
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-header">
                    <div class="col-md-4">
                       <priceLabelingDropdown v-model="priceLebelId" @update:modelValue="GetProductData(priceLebelId)" />
                    </div>
                </div>
                <div class="card-body px-4" v-if="show">
                    <div class="table-responsive pb-5" >
                        <table class="table mb-3">
                            <thead class="thead-light table-hover ">
                                <tr >
                                  
                                    <th class="p-3" >
                                        {{ $t('Code') }}
                                    </th>
                                    <th class="p-3" >
                                        {{ $t('PriceRecord.ProductName') }}
                                    </th>
                                    <th>
                                        {{ $t('Sale Price') }}
                                    </th>
                                    <th>
                                        {{ $t('Purchase Price') }}
                                    </th>
                                    <th>
                                        {{ $t('Cost Price') }}
                                    </th>
                                    <th>
                                        {{ $t('Old Label Price') }}
                                    </th>
                                    <th>
                                        {{ $t('New Label Price') }}
                                    </th>

                                    <th>
                                        {{ $t('PriceRecord.Status') }}
                                    </th>

                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="priceRecord in productList " v-bind:key="priceRecord.id">
                                    <td>{{ priceRecord.code }}</td>
                                    <td class="p-3">
                                        <strong>
                                            <a href="javascript:void(0)">  {{priceRecord.productName}}</a>
                                        </strong>
                                    </td>
                                    <td>
                                        {{ priceRecord.salePrice }}
                                    </td>
                                    <td>
                                        {{ priceRecord.purchasePrice }}
                                    </td>
                                    <td>
                                        {{ priceRecord.costPrice }}
                                        
                                    </td>
                                    <td>
                                        {{ priceRecord.price }}
                                    </td>
                                    <td>
                                        <decimal-to-fixed :textAlignLeft="true" v-model="priceRecord.newPrice" />
                                    </td>
                                    <td>
                                        <input type="checkbox" id="inlineCheckbox1" v-model="priceRecord.isActive" />
                                    </td>
                                </tr>
                            </tbody>
                            <tfoot >
                                <button type="button" class="btn btn-soft-primary btn-sm mt-4" v-on:click="SavePriceRecordChange()">{{ $t('AddPriceRecord.Update') }}</button>
                            </tfoot>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>
</template>

<script>
import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        name: 'PriceRecord',
        components: {
            Loading
        },
        data: function () {
            return {
                arabic: '',
                english: '',
                productList: [],
                priceLebelId:'',
                show:false,
            }
        },
        watch: {
        },
        methods: {
        
            GetProductData: function (priceLebelId) {
                var root = this;
                var url = '/Product/GetPriceRecordChange?priceLebelId=' + priceLebelId.id;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        
                        root.productList = response.data;
                        root.productList.forEach((product) => {
                            product.price = product.newPrice == 0 ? product.price : product.newPrice;
                            product.newPrice = 0.00;
                        });
                        root.loading = false;
                        root.show = true
                    }
                    root.loading = false;
                });
            },
            SavePriceRecordChange: function () {
                
                var root = this;
                var token = '';
                this.loading = true;
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.$https.post('/Product/SavePriceRecordChange', this.productList, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                    if (response.data.IsAddUpdate != '') {
                            root.$swal({
                               title: "Add",
                                text: response.data.IsAddUpdate,
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.close();
                    }
                    else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: response.data.IsAddUpdate,
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 1500,
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
        created: function () {

        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
        }
    }
</script>
