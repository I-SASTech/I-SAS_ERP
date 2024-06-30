<template>
    <div class="row" v-if="isValid('CanViewPaymentOption')">
        

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('PaymentOptions.PaymentOptions') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('PaymentOptions.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('PaymentOptions.PaymentOptions') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddPaymentOption')" v-on:click="AddPaymentOptions"
                                   href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('PaymentOptions.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('PaymentOptions.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-header">
                    <div class="input-group">
                        <button class="btn btn-secondary" type="button" id="button-addon1">
                            <i class="fas fa-search"></i>
                        </button>
                        <input v-model="searchQuery" type="text" class="form-control" :placeholder="$t('PaymentOptions.Search')"
                               aria-label="Example text with button addon" aria-describedby="button-addon1">
                    </div>
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>
                                        #
                                    </th>
                                    <th class="text-center">
                                        {{ $t('PaymentOptions.Image') }}
                                    </th>
                                    <th v-if="english == 'true'" class="text-center">
                                        {{$englishLanguage($t('PaymentOptions.NameEn'))   }}
                                    </th>
                                    <th v-if="isOtherLang()" class="text-center">
                                        {{$arabicLanguage($t('PaymentOptions.NameAr'))   }}
                                    </th>

                                    <th class="text-center">
                                        {{ $t('PaymentOptions.Status') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(details, index) in resultQuery" v-bind:key="details.id">
                                    <td>
                                        {{ index + 1 }}
                                    </td>
                                    <td class="text-center"
                                        v-if="details.image == null || details.image == '' || details.image == undefined">
                                        <img src="Product.png" style="width: 50px;" />
                                    </td>
                                    <td class="text-center" v-else>
                                        <PaymentImageList v-bind:path="details.image" />
                                    </td>
                                    <td v-if="english == 'true'" class="text-center">

                                        <strong>
                                            <a href="javascript:void(0)" v-if="isValid('CanEditPaymentOption')"
                                               v-on:click="EditPaymentOptions(details.id)">{{ details.name }}</a>
                                            <span v-else>{{ details.name }}</span>
                                        </strong>
                                    </td>

                                    <td v-if="isOtherLang()" class="text-center">
                                        <strong>
                                            <a href="javascript:void(0)" v-if="isValid('CanEditPaymentOption')"
                                               v-on:click="EditPaymentOptions(details.id)">{{ details.nameArabic }}</a>
                                            <span v-else>{{ details.nameArabic }}</span>
                                        </strong>

                                    </td>

                                    <td class="text-center">
                                        <span v-if="details.isActive"
                                              class="badge badge-boxed  badge-outline-success">
                                            {{
 $t('PaymentOptions.Active')
                                            }}
                                        </span>
                                        <span v-else class="badge badge-boxed  badge-outline-danger">
                                            {{
                                                $t('PaymentOptions.De-Active')
                                            }}
                                        </span>
                                    </td>

                                </tr>

                            </tbody>
                        </table>
                    </div>
                </div>
            </div>

            <paymentmodel :newpayment="newpayment" :show="show" v-if="show" @close="show = false"
                          @RefreshList="RefreshList" :type="type" />
        </div>

    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>

  </template>
<script>
import clickMixin from '@/Mixins/clickMixin'
export default {
    mixins: [clickMixin],
    data: function () {
        return {
            searchQuery: '',
            show: false,
            loading: false,
            newpayment: {
                id: '',
                name: '',
                nameArabic: '',
                image: '',
                isActive: true
            },
            type: '',
            arabic: '',
            english: '',
            paymentOptionsList: [

            ]

        }
    },
    computed: {
        resultQuery: function () {
            var root = this;
            if (this.searchQuery) {
                return this.paymentOptionsList.filter((pmt) => {
                    return root.searchQuery.toLowerCase().split(' ').every(v => pmt.name.toLowerCase().includes(v) || pmt.nameArabic.toLowerCase().includes(v))
                })
            } else {
                return root.paymentOptionsList;
            }
        },
    },
    methods: {
        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        AddPaymentOptions: function () {
            this.newpayment = {
                id: '00000000-0000-0000-0000-000000000000',
                name: '',
                nameArabic: '',
                image: '',
                isActive: true
            }
            this.show = !this.show;
            this.type = "Add";
        },
        RefreshList: function () {
            this.$router.go();
        },
        GetPaymentOptionsData: function () {
            var root = this;
            var url = '/Product/PaymentOptionsList?isActive=false';
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {

                    root.paymentOptionsList = response.data.paymentOptions;
                }
            });
        },
        EditPaymentOptions: function (id) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Product/PaymentOptionsDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data) {
                        root.newpayment.id = response.data.id;
                        root.newpayment.name = response.data.name;
                        root.newpayment.nameArabic = response.data.nameArabic;
                        root.newpayment.isActive = response.data.isActive;
                        root.newpayment.image = response.data.image;
                        root.show = !root.show;
                        root.type = "Edit"
                    } else {
                        console.log("error: something wrong from db.");
                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });

        }
    },
    created: function () {
        this.$emit('update:modelValue', this.$route.name);
    },
    mounted: function () {

        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        this.GetPaymentOptionsData();
    }
}
</script>