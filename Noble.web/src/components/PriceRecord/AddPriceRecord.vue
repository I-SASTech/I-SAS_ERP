<template>
    <modal :show="show">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type == 'Edit'">{{
                    $t('AddPriceRecord.UpdatePriceRecord') }}</h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>{{ $t('AddPriceRecord.AddPriceRecord') }}
                </h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="form-group has-label col-sm-12 ">
                        <h5>{{ priceRecord.product.code + ' ' + priceRecord.product.englishName + ' ' +
                            priceRecord.product.arabicName }}</h5>
                    </div>

                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">{{ $t('AddPriceRecord.PriceLabeling') }} : <span
                                class="text-danger"> *</span></label>
                        <priceLabelingDropdown v-model="priceLabeling" @update:modelValue="GetPrice(priceLabeling)"
                           :modelValue="priceRecord.priceLabelingId"></priceLabelingDropdown>

                    </div>
                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">{{ $t('AddPriceRecord.PurchasePrice') }} :</label>
                        <input class="form-control " disabled type="text" v-model="priceRecord.product.purchasePrice" />

                    </div>
                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">{{ $t('AddPriceRecord.CostPrice') }} :</label>
                        <input class="form-control " disabled type="text" v-model="priceRecord.product.costPrice" />

                    </div>
                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder"> {{ $t('AddPriceRecord.SalePrice') }}:</label>
                        <input class="form-control " disabled type="text" v-model="priceRecord.product.salePrice" />

                    </div>
                    <div class="form-group has-label col-sm-12 " v-bind:class="{ 'has-danger': v$.priceRecord.price.$error }"
                        :key="randerPrice">
                        <label class="text  font-weight-bolder"> {{ $t('AddPriceRecord.Price') }}: <span
                                class="text-danger"> *</span></label>
                        <decimal-to-fixed v-bind:salePriceCheck="false" :textAlignLeft="true"
                            v-model="v$.priceRecord.price.$model" />
                    </div>


                    <div class="form-group col-md-4">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="priceRecord.isActive">
                            <label for="inlineCheckbox1"> {{ $t('AddPriceRecord.Status') }} </label>
                        </div>
                    </div>

                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SavePriceRecord"
                    v-bind:disabled="v$.priceRecord.$invalid" v-if="type != 'Edit' && isValid('CanAddCategory')">{{
                        $t('AddPriceRecord.Save') }}</button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SavePriceRecord"
                    v-bind:disabled="v$.priceRecord.$invalid" v-if="type == 'Edit' && isValid('CanEditCategory')">{{
                        $t('AddPriceRecord.Update') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{
                    $t('AddPriceRecord.Cancel') }}</button>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>





    </modal>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
export default {
    mixins: [clickMixin],
    props: ['show', 'newPriceRecord', 'type'],
    components: {
        Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
    data: function () {
        return {
            priceRecord: this.newPriceRecord,
            render: 0,
            priceLabeling: [],
            randerPrice: 0,
            arabic: '',
            english: '',
            loading: false,
        }
    },
        validations() {
            return {
                priceRecord: {
                    price: {
                        required
                    },
                    priceLabelingId: {
                        required
                    },
                }
                }
    },
    methods: {
        close: function () {
            this.$emit('close');
        }
        ,
        GetPrice: function (priceLabeling) {

            //this.priceRecord.price = this.$refs.priceLabelingDropdown.GetAmountOfSelected();
            // this.priceRecord.price = priceLabeling.price;
            this.priceRecord.priceLabelingId = priceLabeling.id;
            this.randerPrice++;
        },

        SavePriceRecord: function () {

            var root = this;
            var token = '';
            this.loading = true;
            if (token == '') {
                token = localStorage.getItem('token');
            }
            
            if (root.type != "Edit") 
            {
                var priceLabelingRecord = this.$store.isPriceLabelingRecordList;
                if (priceLabelingRecord !== null && priceLabelingRecord.length > 0) {
                    var priceRecordObj = priceLabelingRecord.find(x=> x.id == this.priceRecord.product.id);
                    if(priceRecordObj.priceRecordLookupModel != null)
                    {
                        var samePriceRecord = priceRecordObj.priceRecordLookupModel.find(x=> x.priceLabelingId == this.priceRecord.priceLabelingId);

                        if(samePriceRecord != null)
                        {
                            this.loading = false;
                            return (
                                root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Your Price Label  Already Exist!' : 'اسم الأصل الخاص بك موجود بالفعل!',
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                                })
                            )
                        }
                    }
                }
            }

            this.priceRecord.productId = this.priceRecord.product.id
            this.$https.post('/Product/SavePriceRecordInformation', this.priceRecord, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data.isSuccess == true) {
                        if (root.type != "Edit") {

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
                        }
                        else {

                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update Successfully!' : 'تم التحديث بنجاح',
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.close();
                        }
                    }
                    else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Your Origin Name  Already Exist!' : 'اسم الأصل الخاص بك موجود بالفعل!',
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
    mounted: function () {
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');


    }
}
</script>
