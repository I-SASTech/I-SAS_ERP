<template>
    <modal :show="show" v-if=" isValid('CanAddAllowance') || isValid('CanEditAllowance') ">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type == 'Edit'">{{
                        $t('AddAllowanceType.UpdateAllowanceType')
                }}</h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>{{ $t('AddAllowanceType.AddAllowanceType') }}
                </h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div v-if="english == 'true'" class="form-group has-label col-sm-12 "
                        v-bind:class="{ 'has-danger': v$.allowanceType.name.$error }">
                        <label class="text  font-weight-bolder"> {{ $t('AddAllowanceType.NameEnglish') }}: <span
                                class="text-danger"> *</span></label>
                        <input class="form-control" v-model="v$.allowanceType.name.$model" type="text" />
                    </div>
                    <div v-if="isOtherLang()" class="form-group has-label col-sm-12 "
                        v-bind:class="{ 'has-danger': v$.allowanceType.nameArabic.$error }">
                        <label class="text  font-weight-bolder">{{ $t('AddAllowanceType.NameArabic') }}: <span
                                class="text-danger"> *</span></label>
                        <input class="form-control  " v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'"
                            v-model="v$.allowanceType.nameArabic.$model" type="text" />
                    </div>

                    <div class="form-group has-label col-sm-12 "
                        v-bind:class="{ 'has-danger': v$.allowanceType.description.$error }">
                        <label class="text  font-weight-bolder"> {{ $t('AddAllowanceType.Description') }}: </label>
                        <textarea class="form-control" v-model="v$.allowanceType.description.$model" type="text" />
                    </div>
                    <div class="form-group col-md-4">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="allowanceType.isActive">
                            <label for="inlineCheckbox1"> {{ $t('AddAllowanceType.Status') }} </label>
                        </div>
                    </div>

                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveAllowanceType"
                    v-bind:disabled="v$.allowanceType.$invalid"
                    v-if="type != 'Edit' && isValid('CanAddAllowanceType')">{{
                            $t('AddAllowanceType.Save')
                    }}</button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveAllowanceType"
                    v-bind:disabled="v$.allowanceType.$invalid"
                    v-if="type == 'Edit' && isValid('CanEditAllowanceType')">{{
                            $t('AddAllowanceType.Update')
                    }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{
                        $t('AddAllowanceType.Cancel')
                }}</button>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>



    </modal>
    <acessdenied v-else :model=true></acessdenied>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
  import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

import { requiredIf, maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
export default {
    props: ['show', 'newAllowanceType', 'type'],
    mixins: [clickMixin],
      setup() {
            return { v$: useVuelidate() }
        },
  components: {
            Loading
        },
    data: function () {
        return {
             loading: false,
            allowanceType: this.newAllowanceType,
            render: 0,
            arabic: '',
            english: '',
        }
    },
    validations() {
      return{
          allowanceType: {
            name: {
                maxLength: maxLength(50)
            },
            nameArabic: {
                required: requiredIf(function () {
                            return !this.allowanceType.name;
                        }),
                // required: requiredIf((x) => {
                //     if (x.name == '' || x.name == null)
                //         return true;
                //     return false;
                // }),
                maxLength: maxLength(50)
            },

            description: {
                maxLength: maxLength(200)
            }
        }
      }
    },
    methods: {
        close: function () {
            this.$emit('close');
        },
        SaveAllowanceType: function () {
              this.loading = true;
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.$https.post('/Payroll/SaveAllowanceType', this.allowanceType, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                if (response.data.isSuccess == true) {

                    if (root.type != "Edit") {
                        root.$store.isAllowanceTypeList.push({
                            id: response.data.allowanceType.id,
                            name: response.data.allowanceType.name,
                            nameArabic: response.data.allowanceType.nameArabic,
                            description: response.data.allowanceType.description,
                            isActive: response.data.allowanceType.isActive,
                        })
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
                        var data = root.$store.isAllowanceTypeList.find(function (x) {
                            return x.id == response.data.allowanceType.id;
                        });
                        data.id = response.data.allowanceType.id;
                        data.name = response.data.allowanceType.name;
                        data.nameArabic = response.data.allowanceType.nameArabic;
                        data.description = response.data.allowanceType.description;
                        data.isActive = response.data.allowanceType.isActive;
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
                        text: "Your Allowance Type Name  Already Exist!",
                        type: 'error',
                        icon: 'error',
                        showConfirmButton: false,
                        timer: 1500,
                        timerProgressBar: true,
                    });
                }
            })
                .catch(error => {

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
                .finally(() => root.loading = false);
        }
    },
    mounted: function () {
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        

    }
}
</script>
