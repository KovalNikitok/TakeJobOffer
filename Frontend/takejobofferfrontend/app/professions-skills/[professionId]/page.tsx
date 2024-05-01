import { getAllProfessionsISR } from "../../services/professions";
import ProfessionSkillsClient  from "./ProfessionSkillsPage";

export const revalidate = 600;
export const dynamicParams = true;

export async function generateStaticParams () {
  let professions: Profession[];
  try {
    professions = await getAllProfessionsISR(revalidate);
    return professions.map(profession => ({
      params: { professionId: profession.id },
    }));
  }
  catch (error) {
    console.log("Profession not found!" + `\n ${error}`);
    return [{ params: { professionId: "" }}];
  }
}

export default async function ProfessionSkillsPage({ params }: { params: { professionId: string } }) {
  const { professionId } = params;

  return (
    <ProfessionSkillsClient professionId={professionId} />
  );
}